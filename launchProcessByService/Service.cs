using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RpcLibrary;
using RpcTransport;
using Win32NS;
using System.Runtime.InteropServices;

namespace launchProcessByService
{
  public class MyService
  {
    private RpcServerApi source;

    public void active(string endpoint)
    {
      // The client and server must agree on the interface id to use:
      var iid = new Guid("{78323803-786f-4f7b-908d-b2e89c41d45f}");

      // Create the server instance, adjust the defaults to your needs.
      source = new RpcServerApi(iid, 100, ushort.MaxValue, allowAnonTcp: false);

      try
      {
        // Add an endpoint so the client can connect, this is local-host only:
        source.AddProtocol(RpcProtseq.ncalrpc, endpoint, 100);

        // If you want to use TCP/IP uncomment the following, make sure your client authenticates or allowAnonTcp is true
        // server.AddProtocol(RpcProtseq.ncacn_ip_tcp, @"8080", 25);

        // Add the types of authentication we will accept
        source.AddAuthentication(RpcAuthentication.RPC_C_AUTHN_GSS_NEGOTIATE);
        source.AddAuthentication(RpcAuthentication.RPC_C_AUTHN_WINNT);
        source.AddAuthentication(RpcAuthentication.RPC_C_AUTHN_NONE);

        source.OnExecute += executeDispatch;

        source.OnExecuteAsync += executeAsyncDispatch;

        source.StartListening();
      }
      catch (Exception e)
      {
        throw;
      }
    }

    public void deactive()
    {
      if (source != null)
      {
        source.Dispose();
      }
      source = null;
    }

    private byte[] executeDispatch(IRpcClientInfo client, byte[] input)
    {
      using (client.Impersonate())
      {
        IntPtr hToken;

        Win32.OpenThreadToken(Win32.GetCurrentThread(), Win32.TOKEN_READ | Win32.TOKEN_IMPERSONATE, false, out hToken);

        Win32.SECURITY_ATTRIBUTES saProcessAttributes = new Win32.SECURITY_ATTRIBUTES();
        Win32.SECURITY_ATTRIBUTES saThreadAttributes = new Win32.SECURITY_ATTRIBUTES();

        Win32.STARTUPINFO startupInfo = new Win32.STARTUPINFO();
        startupInfo.cb = Marshal.SizeOf(startupInfo);

        Win32.PROCESS_INFORMATION processInfo = new Win32.PROCESS_INFORMATION();

        bool result = Win32.CreateProcessAsUser(
          hToken, @"c:\windows\notepad.exe", null,
          ref saProcessAttributes, ref saThreadAttributes, true, 0x01000000, IntPtr.Zero, null, ref startupInfo, out processInfo);

        if (!result)
        {
          int error = Marshal.GetLastWin32Error();
        }

        return new byte[] { 0 };
      }
    }

    private byte[] executeAsyncDispatch(IRpcClientInfo client, IntPtr pAsyncState, byte[] input)
    {
      using (client.Impersonate())
      {
        IntPtr hToken;

        Win32.OpenThreadToken(Win32.GetCurrentThread(), Win32.TOKEN_READ | Win32.TOKEN_IMPERSONATE, false, out hToken);

        Win32.SECURITY_ATTRIBUTES saProcessAttributes = new Win32.SECURITY_ATTRIBUTES();
        Win32.SECURITY_ATTRIBUTES saThreadAttributes = new Win32.SECURITY_ATTRIBUTES();

        Win32.STARTUPINFO startupInfo = new Win32.STARTUPINFO();
        startupInfo.cb = Marshal.SizeOf(startupInfo);

        Win32.PROCESS_INFORMATION processInfo = new Win32.PROCESS_INFORMATION();

        bool result = Win32.CreateProcessAsUser(
          hToken, @"c:\windows\notepad.exe", null,
          ref saProcessAttributes, ref saThreadAttributes, true, 0, IntPtr.Zero, null, ref startupInfo, out processInfo);

        return new byte[] { 0 };
      }
    }
  }
}