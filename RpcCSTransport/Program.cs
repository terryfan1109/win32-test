using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RpcLibrary;
using RpcTransport;

namespace RpcCSTransport
{
  class Program
  {
    static void Main(string[] args)
    {
      RpcLibrary.Log.VerboseEnabled = true;
      // The client and server must agree on the interface id to use:
      var iid = new Guid("{f4db45dc-0dcb-4003-b680-56c40f6cb6a8}");

      bool attempt = true;
      while (attempt)
      {
        attempt = false;
        // Open the connection based on the endpoint information and interface IID
        using (var client = new RpcClientApi(iid, RpcProtseq.ncalrpc, null, "1234"))
        //using (var client = new RpcClientApi(iid, RpcProtseq.ncacn_ip_tcp, null, @"18081"))
        {
          // Provide authentication information (not nessessary for LRPC)
          client.AuthenticateAs(RpcClientApi.Self);

          // Send the request and get a response
          try
          {
            var response = client.Execute(Encoding.UTF8.GetBytes(args.Length == 0 ? "Greetings" : args[0]));
            Console.WriteLine("Server response: {0}", Encoding.UTF8.GetString(response));

            var task = client.ExecuteAsync(Encoding.UTF8.GetBytes(args.Length == 0 ? "Greetines" : args[0])).
              ContinueWith<String>( r =>
              {
                return Encoding.UTF8.GetString(r.Result);
              });

            task.Wait();

            Console.WriteLine("Server response by async way: {0}", task.Result);
            
          }
          catch (RpcException rx)
          {
            if (rx.RpcError == RpcError.RPC_S_SERVER_UNAVAILABLE || rx.RpcError == RpcError.RPC_S_SERVER_TOO_BUSY)
            {
              //Use a wait handle if your on the same box...
              Console.Error.WriteLine("Waiting for server...");
              System.Threading.Thread.Sleep(1000);
              attempt = true;
            }
            else
              Console.Error.WriteLine(rx);
          }
          catch (Exception ex)
          {
            Console.Error.WriteLine(ex);
          }
        }
      }
      // done...
      Console.WriteLine("Press [Enter] to exit...");
      Console.ReadLine();
    }
  }
}
