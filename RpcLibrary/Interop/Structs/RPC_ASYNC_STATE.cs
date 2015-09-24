using System;
using System.Runtime.InteropServices;

namespace RpcLibrary.Interop.Structs
{
  [StructLayout(LayoutKind.Sequential)]
  public struct RPC_ASYNC_STATE
  {
    public uint    Size; // size of this structure
    public uint    Signature;
    public int     Lock;
    public uint    Flags;
    public IntPtr  StubInfo;
    public IntPtr  UserInfo;
    public IntPtr  RuntimeInfo;
    public RpcAsyncEvent Event;
    public RpcNotificationTypes NotificationType;
    public RPC_ASYNC_NOTIFICATION_INFO u;
    public IntPtr  Reserved1;
    public IntPtr  Reserved2;
    public IntPtr  Reserved3;
    public IntPtr  Reserved4;
  }
}
