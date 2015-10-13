using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpTest.Net.RpcLibrary;

namespace rpcCSharpClient
{
  class Program
  {
    static void Test()
    {
      //An id to identify the endpoint interface
      Guid iid = Guid.Parse("430436ab-8786-4b19-905f-2e5cc11edda2");
      
      //For the client, we specify the protocol, endpoint, and interface id to connect
      using (RpcClientApi client = new RpcClientApi(iid, RpcProtseq.ncalrpc, null, "4747"))
      {
        client.AuthenticateAs(RpcClientApi.Self);

        byte[] response = client.Execute(new byte[0]);
      }
    }

    static void Main(string[] args)
    {
    }
  }
}
