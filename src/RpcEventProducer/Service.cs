using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProtoBuf;
using RpcLibrary;
using RpcLibrary.Interop.Structs;
using RpcTransport;
using Rpc.Event.Protocol;
using System.IO;

namespace Rpc.Event.Producer
{
  class MyService
  {
    static private List<AsyncContext> eventSubscriptions = new List<AsyncContext>();

    static void Main(string[] args)
    {
      var iid = new Guid("{04f5e707-d969-4822-95c5-2f3d4fd47f77}");

      using (var server = new RpcServerApi2(iid, 100, ushort.MaxValue, allowAnonTcp: false))
      {
        try
        {
          server.AddProtocol(RpcProtseq.ncalrpc, "3242", 100);

          server.AddAuthentication(RpcAuthentication.RPC_C_AUTHN_GSS_NEGOTIATE);
          server.AddAuthentication(RpcAuthentication.RPC_C_AUTHN_WINNT);
          server.AddAuthentication(RpcAuthentication.RPC_C_AUTHN_NONE);

          server.OnExecute += execute;
          server.OnExecuteAsync += executeAsync2;

          server.StartListening();
        }
        catch (Exception ex)
        {
          Console.Error.WriteLine(ex);
          throw;
        }

        // Wait until you are done...
        Console.WriteLine("Press [Enter] to exit...");
        Console.ReadLine();

        fireShutDownEvent();
      }
    }

    private static byte[] execute(IRpcClientInfo client, byte[] bytes)
    {
      using (client.Impersonate())
      {
        var request = Serializer.Deserialize<Request>(new MemoryStream(bytes));
        if (0 == request.value % 5)
        {
          fireEvent();
        }
        var response = new Response();
        return response.toByteArray();
      }
    }

    private static byte[] executeAsync(IRpcClientInfo client, IntPtr asyncState, byte[] bytes)
    {
      using (client.Impersonate())
      {
        var request = Serializer.Deserialize<EventRequest>(new MemoryStream(bytes));
        var response = new EventResponse();

        return response.toByteArray();
      }
    }

    private static void executeAsync2(IRpcClientInfo client, AsyncContext asyncState, byte[] bytes)
    {
      using (client.Impersonate())
      {
        var request = Serializer.Deserialize<EventRequest>(new MemoryStream(bytes));

        addSubscription(asyncState);
      }
    }

    private static void addSubscription(AsyncContext asyncContext)
    {
      lock (asyncContext)
      {
        eventSubscriptions.Add(asyncContext);
      }
    }

    private static void fireEvent()
    {
      AsyncContext[] subscriptions;
      lock (eventSubscriptions)
      {
        subscriptions = new AsyncContext[eventSubscriptions.Count];
        eventSubscriptions.CopyTo(subscriptions);
        eventSubscriptions.Clear();
      }

      if (0 < subscriptions.Length)
      {
        var response = new EventResponse();
        foreach (var elm in subscriptions)
        {
          try
          {
            elm.completeCall(response.toByteArray());
          }
          catch (Exception e)
          {
          }
        }
      }
    }

    private static void fireShutDownEvent()
    {
      AsyncContext[] subscriptions;
      lock (eventSubscriptions)
      {
        subscriptions = new AsyncContext[eventSubscriptions.Count];
        eventSubscriptions.CopyTo(subscriptions);
        eventSubscriptions.Clear();
      }

      if (0 < subscriptions.Length)
      {
        foreach (var elm in subscriptions)
        {
          try
          {
            elm.completeCall((uint) RpcError.RPC_S_SERVER_UNAVAILABLE);
          }
          catch (Exception e)
          {
          }
        }
      }
    }

  }
}
