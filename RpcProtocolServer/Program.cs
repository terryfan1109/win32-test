using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using ProtoBuf;
using RpcLibrary;
using RpcLibrary.Interop.Structs;
using RpcTransport;
using RpcProto;

namespace RpcProtocolServer
{
  class Program
  {
    static byte[] toBytes<T>(T instance)
    {
      var stream = new MemoryStream();
      ProtoBuf.Serializer.Serialize(stream, instance);
      stream.Seek(0, SeekOrigin.Begin);
      var buf = new byte[stream.Length];
      stream.Read(buf, 0, buf.Length);
      return buf;
    } 

    static void Main(string[] args)
    {
      // The client and server must agree on the interface id to use:
      var iid = new Guid("{0092F74D-0EA7-4667-A89F-A04C64244031}");

      // Create the server instance, adjust the defaults to your needs.
      using (var server = new RpcServerApi(iid, 100, ushort.MaxValue, allowAnonTcp: false))
      {
        try
        {
          // Add an endpoint so the client can connect, this is local-host only:
          server.AddProtocol(RpcProtseq.ncalrpc, "5678", 100);

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
                  Console.WriteLine("Received'{0}' from {1} ...", bytes.Length, client.ClientUser.Name);
                  byte[] response;

                  var memoryStream = new MemoryStream(bytes);
                  RpcProto.Request message;
                  try
                  {
                    message = ProtoBuf.Serializer.Deserialize<RpcProto.Request>(memoryStream);
                  }
                  catch (Exception e)
                  {
                    Console.WriteLine(e);
                    throw e;
                  }

                  switch (message.method)
                  {
                    case RpcProto.RequestType.GetCustomers:
                      var r = message.post;
                      Console.WriteLine("Received '{0}'", r);
                      var rpn = new RpcProto.GetCustomerResponse() {
                        result = 0,
                      };
                      response = toBytes(rpn);
                      break;


                    default:
                      response = new byte[0];
                      break;
                  }

                  return response;
                }
              };

          server.OnExecuteAsync +=
            delegate(IRpcClientInfo client, IntPtr pAsyncState, byte[] bytes)
            {
              //Impersonate the caller:
              using (client.Impersonate())
              {
                Console.WriteLine("Received'{0}' from {1} ...", bytes.Length, client.ClientUser.Name);

                byte[] response;
                
                var memoryStream = new MemoryStream(bytes);
                RpcProto.Request message;
                try
                {
                  message = ProtoBuf.Serializer.Deserialize<RpcProto.Request>(memoryStream);
                }
                catch (Exception e)
                {
                  Console.WriteLine(e);
                  throw e;
                }
                
                switch (message.method)
                {
                  case RpcProto.RequestType.PostCustomer:
                    var r1 = message.post;
                    Console.WriteLine("Received '{0}'", r1);
                    var rpn1 = new RpcProto.PostCustomerResponse() {
                      result = 0
                    };
                    response = toBytes(rpn1);
                    break;

                  case RpcProto.RequestType.DeleteCustomer: { }
                    var r2 = message.delete;
                    Console.WriteLine("Received '{0}'", r2);
                    var rpn2 = new RpcProto.DeleteCustomerResponse() {
                      result = 0
                    };
                    response = toBytes(rpn2);
                    break;

                  default:
                    response = new byte[0];
                    break;
                }

                return response;
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
