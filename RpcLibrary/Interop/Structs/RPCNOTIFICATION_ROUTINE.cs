using System;
using System.Runtime.InteropServices;

namespace RpcLibrary.Interop.Structs
{
  [StructLayout(LayoutKind.Sequential)]
  public struct RPCNOTIFICATION_ROUTINE
  {
    IntPtr pAsync;
    IntPtr Context;
    RpcAsyncEvent Event;
  }
}
