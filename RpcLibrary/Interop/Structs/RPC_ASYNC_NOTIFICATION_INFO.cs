using System;
using System.Runtime.InteropServices;

namespace RpcLibrary.Interop.Structs
{
  [StructLayout(LayoutKind.Sequential)]
  public struct APC_TAG
  {
    public IntPtr NotificationRoutine;
    public IntPtr hThread;
  };

  [StructLayout(LayoutKind.Sequential)]
  public struct IOCP_TAG
  {
    public IntPtr hIOPort;
    public uint dwNumberOfBytesTransferred;
    public IntPtr dwCompletionKey;
    public IntPtr lpOverlapped;
  };

  [StructLayout(LayoutKind.Sequential)]
  public struct HWND_TAG
  {
    public IntPtr hWnd;
    public uint Msg;
  };

  [StructLayout(LayoutKind.Explicit)]
  public struct RPC_ASYNC_NOTIFICATION_INFO
  {
    [FieldOffset(0)]
    public APC_TAG APC;
    [FieldOffset(0)]
    public IOCP_TAG IOCP;
    [FieldOffset(0)]
    public HWND_TAG HWND;
    [FieldOffset(0)]
    public IntPtr hEvent;
    [FieldOffset(0)]
    public IntPtr NotificationRoutine;
  }
}
