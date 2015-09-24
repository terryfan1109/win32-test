using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RpcLibrary;
using RpcLibrary.Interop.Structs;
using RpcTransport;
using System.IO;

namespace RpcProtocolClient
{
  class Program
  {
    static byte[] Execute(RpcClientApi client, Stream stream)
    {
      byte[] buffer = new byte[stream.Length];

      stream.Seek(0, SeekOrigin.Begin);
      stream.Read(buffer, 0, buffer.Length);

      return client.Execute(buffer);
    }

    static Task<byte[]> ExecuteAsync(RpcClientApi client, Stream stream)
    {
      byte[] buf = new byte[stream.Length];
      stream.Seek(0, SeekOrigin.Begin);
      stream.Read(buf, 0, buf.Length);

      return client.ExecuteAsync(buf);
    }


    static List<RpcProto.Customer> GetCustomers(RpcClientApi client)
    {
      var request = new RpcProto.Request()
      {
        method =  RpcProto.RequestType.GetCustomers,
        get = new RpcProto.GetCustomerRequest()
      };
      var stream = new MemoryStream();

      ProtoBuf.Serializer.Serialize(stream, request);
      var output = Execute(client, stream);

      var response = ProtoBuf.Serializer.Deserialize<RpcProto.GetCustomerResponse>(new MemoryStream(output));
      if (0 != response.result)
      {
        throw new Exception(response.ToString());
      }

      return response.customers;
    }

    static void AddCustomer(RpcClientApi client, RpcProto.Customer customer)
    {
      var request = new RpcProto.Request()
      {
        method = RpcProto.RequestType.PostCustomer,
        post = new RpcProto.PostCustomerRequest()
        {
          customer = customer
        }
      };

      var stream = new MemoryStream();
      ProtoBuf.Serializer.Serialize(stream, request);

      var task = ExecuteAsync(client, stream).ContinueWith(r =>
      {
        var response = ProtoBuf.Serializer.Deserialize<RpcProto.PostCustomerResponse>(
          new MemoryStream(r.Result));

        if (0 != response.result)
        {
          throw new Exception(response.ToString());
        }
      });

      task.Wait();
    }

    static void RemoveCustomer(RpcClientApi client, RpcProto.Customer customer)
    {
      var request = new RpcProto.Request()
      {
        method = RpcProto.RequestType.DeleteCustomer,
        delete = new RpcProto.DeleteCustomerRequest()
        {
          customer = customer
        }
      };

      var stream = new MemoryStream();
      ProtoBuf.Serializer.Serialize(stream, request);

      var task = ExecuteAsync(client, stream).ContinueWith(r =>
      {
        var response = ProtoBuf.Serializer.Deserialize<RpcProto.DeleteCustomerResponse>(
          new MemoryStream(r.Result));

        if (0 != response.result)
        {
          throw new Exception(response.ToString());
        }
      });

      task.Wait();
    }

    static void Main(string[] args)
    {
      RpcLibrary.Log.VerboseEnabled = true;
      // The client and server must agree on the interface id to use:
      var iid = new Guid("{0092F74D-0EA7-4667-A89F-A04C64244031}");

      bool attempt = true;
      while (attempt)
      {
        attempt = false;
        // Open the connection based on the endpoint information and interface IID
        using (var client = new RpcClientApi(iid, RpcProtseq.ncalrpc, null, "5678"))
        {
          //using (var client = new RpcClientApi(iid, RpcProtseq.ncacn_ip_tcp, null, @"18081"))
          client.AuthenticateAs(RpcClientApi.Self);

          // Send the request and get a response
          try
          {
            GetCustomers(client);

            AddCustomer(client, new RpcProto.Customer() { firstName = "Jack", lastName = "Dancer" });

            RemoveCustomer(client, new RpcProto.Customer() { firstName = "Marry", lastName = "Player" });
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
