using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RpcLibrary;
using RpcLibrary.Interop.Structs;
using RpcTransport;
using System.Threading;

namespace RpcCSTransportServer
{
  class Program
  {
    static void Main(string[] args)
    {
      // The client and server must agree on the interface id to use:
      var iid = new Guid("{f4db45dc-0dcb-4003-b680-56c40f6cb6a8}");

      // Create the server instance, adjust the defaults to your needs.
      using (var server = new RpcServerApi(iid, 100, ushort.MaxValue, allowAnonTcp: false))
      {
        try
        {
          // Add an endpoint so the client can connect, this is local-host only:
          server.AddProtocol(RpcProtseq.ncalrpc, "1234", 100);

          // If you want to use TCP/IP uncomment the following, make sure your client authenticates or allowAnonTcp is true
          // server.AddProtocol(RpcProtseq.ncacn_ip_tcp, @"8080", 25);

          // Add the types of authentication we will accept
          server.AddAuthentication(RpcAuthentication.RPC_C_AUTHN_GSS_NEGOTIATE);
          server.AddAuthentication(RpcAuthentication.RPC_C_AUTHN_WINNT);
          server.AddAuthentication(RpcAuthentication.RPC_C_AUTHN_NONE);

          // Subscribe the code to handle requests on this event:
          server.OnExecute +=
              delegate(IRpcClientInfo client, byte[] bytes)
              {
                //Impersonate the caller:
                using (client.Impersonate())
                {
                  var reqBody = Encoding.UTF8.GetString(bytes);
                  Console.WriteLine("Received '{0}' from {1}", reqBody, client.ClientUser.Name);

                  return Encoding.UTF8.GetBytes(
                      String.Format(
                          "Hello {0}, I received your message '{1}'.",
                          client.ClientUser.Name,
                          reqBody
                          )
                      );
                }
              };

          server.OnExecuteAsync +=
            delegate(IRpcClientInfo client, IntPtr pAsyncState, byte[] bytes)
            {
                //Impersonate the caller:
                using (client.Impersonate())
                {
                  var reqBody = Encoding.UTF8.GetString(bytes);
                  Console.Write("Received (async) '{0}' from {1} ...", reqBody, client.ClientUser.Name);
                  Thread.Sleep(3 * 1000);
                  Console.WriteLine("Wake up!");
                  return Encoding.UTF8.GetBytes(
                      String.Format(
                          "Hello {0}, I received your message '{1}'.",
                          client.ClientUser.Name,
                          reqBody
                          )
                      );
                }
            };


          // Start Listening 
          server.StartListening();
        }
        catch (Exception ex)
        {
          Console.Error.WriteLine(ex);
        }

        // Wait until you are done...
        Console.WriteLine("Press [Enter] to exit...");
        Console.ReadLine();
      }
    }
  }
}
