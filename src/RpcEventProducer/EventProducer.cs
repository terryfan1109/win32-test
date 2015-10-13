using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RpcLibrary;
using RpcLibrary.Interop.Structs;
using RpcTransport;

namespace Rpc.Event.Producer
{
  //class Subscribtion
  //{
  //  IRpcClientInfo client { get; set; }
  //  IntPtr asyncState { get; set; }
  //}

  //class EventProducer
  //{
  //  private List<Subscribtion> subscribers;

  //  void subscribe(IRpcClientInfo client, IntPtr asyncState)
  //  {
  //    subscribers.Add(new Subscribtion() { client = client, asyncState = asyncState });
  //  }

  //  void fireEvent()
  //  {
  //    subscribers.ForEach(() =>
  //    {
  //      RpcServerApi.RpcAsyncCompleteCall(asyncState, ref result);
  //    });
  //  }
  //}
}
