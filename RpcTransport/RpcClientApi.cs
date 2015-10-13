#region Copyright 2010-2014 by Roger Knapp, Licensed under the Apache License, Version 2.0
/* Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *   http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
#endregion
using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using RpcLibrary;
using RpcLibrary.Interop;
using RpcLibrary.Interop.Structs;
using RpcTransport.Interop.Structs;

namespace RpcTransport
{
  /// <summary>
  /// Provides a connection-based wrapper around the RPC client
  /// </summary>
  [System.Diagnostics.DebuggerDisplay("{_handle} @{_binding}")]
  public class RpcClientApi : IDisposable
  {
    /// <summary> The interface Id the client is connected to </summary>
    public readonly Guid IID;
    private readonly RpcProtseq _protocol;
    private readonly string _binding;
    private readonly RpcHandle _handle;
    private bool _authenticated;

    /// <summary>
    /// Connects to the provided server interface with the given protocol and server:endpoint
    /// </summary>
    public RpcClientApi(Guid iid, RpcProtseq protocol, string server, string endpoint)
    {
      _handle = new RpcClientHandle();
      IID = iid;
      _protocol = protocol;
      Log.Verbose("RpcClient('{0}:{1}')", server, endpoint);

      _binding = StringBindingCompose(protocol, server, endpoint, null);
      Connect();
    }
    /// <summary>
    /// Disconnects the client and frees any resources.
    /// </summary>
    public void Dispose()
    {
      Log.Verbose("RpcClient('{0}').Dispose()", _binding);
      _handle.Dispose();
    }
    /// <summary>
    /// Returns a constant NetworkCredential that represents the Anonymous user
    /// </summary>
    public static NetworkCredential Anonymous
    {
      get { return new NetworkCredential("ANONYMOUS LOGON", "", "NT_AUTHORITY"); }
    }
    /// <summary>
    /// Returns a constant NetworkCredential that represents the current Windows user
    /// </summary>
    public static NetworkCredential Self
    {
      get { return null; }
    }
    /// <summary>
    /// The protocol that was provided to the constructor
    /// </summary>
    public RpcProtseq Protocol
    {
      get { return _protocol; }
    }
    /// <summary>
    /// Connects the client; however, this is a soft-connection and validation of 
    /// the connection will not take place until the first call is attempted.
    /// </summary>
    private void Connect()
    {
      BindingFromStringBinding(_handle, _binding);
      Log.Verbose("RpcClient.Connect({0} = {1})", _handle.Handle, _binding);
    }
    /// <summary>
    /// Adds authentication information to the client, use the static Self to
    /// authenticate as the currently logged on Windows user.
    /// </summary>
    public void AuthenticateAs(NetworkCredential credentials)
    {
      AuthenticateAs(null, credentials);
    }
    /// <summary>
    /// Adds authentication information to the client, use the static Self to
    /// authenticate as the currently logged on Windows user.
    /// </summary>
    public void AuthenticateAs(string serverPrincipalName, NetworkCredential credentials)
    {
      RpcAuthentication[] types = new RpcAuthentication[] { RpcAuthentication.RPC_C_AUTHN_GSS_NEGOTIATE, RpcAuthentication.RPC_C_AUTHN_WINNT };
      RpcProtectionLevel protect = RpcProtectionLevel.RPC_C_PROTECT_LEVEL_PKT_PRIVACY;

      bool isAnon = (credentials != null && credentials.UserName == Anonymous.UserName && credentials.Domain == Anonymous.Domain);
      if (isAnon)
      {
        protect = RpcProtectionLevel.RPC_C_PROTECT_LEVEL_DEFAULT;
        types = new RpcAuthentication[] { RpcAuthentication.RPC_C_AUTHN_NONE };
        credentials = null;
      }

      AuthenticateAs(serverPrincipalName, credentials, protect, types);
    }
    /// <summary>
    /// Adds authentication information to the client, use the static Self to
    /// authenticate as the currently logged on Windows user.  This overload allows
    /// you to specify the privacy level and authentication types to try. Normally
    /// these default to RPC_C_PROTECT_LEVEL_PKT_PRIVACY, and both RPC_C_AUTHN_GSS_NEGOTIATE
    /// or RPC_C_AUTHN_WINNT if that fails.  If credentials is null, or is the Anonymous
    /// user, RPC_C_PROTECT_LEVEL_DEFAULT and RPC_C_AUTHN_NONE are used instead.
    /// </summary>
    public void AuthenticateAs(string serverPrincipalName, NetworkCredential credentials, RpcProtectionLevel level, params RpcAuthentication[] authTypes)
    {
      if (!_authenticated)
      {
        BindingSetAuthInfo(level, authTypes, _handle, serverPrincipalName, credentials);
        _authenticated = true;
      }
    }
    /// <summary>
    /// Sends a message as an array of bytes and retrieves the response from the server, if
    /// AuthenticateAs() has not been called, the client will authenticate as Anonymous.
    /// </summary>
    public byte[] Execute(byte[] input)
    {
      if (!_authenticated)
      {
        Log.Warning("AuthenticateAs was not called, assuming Anonymous.");
        AuthenticateAs(Anonymous);
      }
      Log.Verbose("RpcExecute(byte[{0}])", input.Length);
      return InvokeRpc(_handle, IID, input);
    }

    public struct ExecuteAsyncResponse
    {
      public RpcError result;
      public byte[] response;
    }

    /// <summary>
    /// Send a message asynchronous
    /// </summary>
    /// 
    public Task<ExecuteAsyncResponse> ExecuteAsync(byte[] input)
    {
      return Task.Factory.StartNew( () =>
      {
        if (!_authenticated)
        {
          Log.Warning("AuthenticateAs was not called, assuming Anonymous.");
          AuthenticateAs(Anonymous);
        }
        Log.Verbose("RpcExecute(byte[{0}])", input.Length);

        var evnt = new ManualResetEvent(false);

        ExecuteAsyncResponse result;
        result.result = RpcError.RPC_E_FAIL;
        result.response = null;

        InvokeRpcAsync(_handle, IID, input, (r) => { result = r; evnt.Set(); });
        evnt.WaitOne();

        return result;
      });

    }

    /* ********************************************************************
     * WinAPI INTEROP
     * *******************************************************************/

    private class RpcClientHandle : RpcHandle
    {
      protected override void DisposeHandle(ref IntPtr handle)
      {
        if (handle != IntPtr.Zero)
        {
          RpcException.Assert(RpcApi.RpcBindingFree(ref Handle));
          handle = IntPtr.Zero;
        }
      }
    }

    private static String StringBindingCompose(RpcProtseq ProtSeq, String NetworkAddr, String Endpoint,
                                               String Options)
    {
      IntPtr lpBindingString;
      RpcError result = RpcApi.RpcStringBindingCompose(null, ProtSeq.ToString(), NetworkAddr, Endpoint, Options,
                                                out lpBindingString);
      RpcException.Assert(result);

      try
      {
        return Marshal.PtrToStringUni(lpBindingString);
      }
      finally
      {
        RpcException.Assert(RpcApi.RpcStringFree(ref lpBindingString));
      }
    }

    private static void BindingFromStringBinding(RpcHandle handle, String bindingString)
    {
      RpcError result = RpcApi.RpcBindingFromStringBinding(bindingString, out handle.Handle);
      RpcException.Assert(result);
    }

    private static void BindingSetAuthInfo(RpcProtectionLevel level, RpcAuthentication[] authTypes,
        RpcHandle handle, string serverPrincipalName, NetworkCredential credentails)
    {
      if (credentails == null)
      {
        foreach (RpcAuthentication atype in authTypes)
        {
          RpcError result = RpcApi.RpcBindingSetAuthInfo2(handle.Handle, serverPrincipalName, level, atype, IntPtr.Zero, 0);
          if (result != RpcError.RPC_S_OK)
            Log.Warning("Unable to register {0}, result = {1}", atype, new RpcException(result).Message);
        }
      }
      else
      {
        SEC_WINNT_AUTH_IDENTITY pSecInfo = new SEC_WINNT_AUTH_IDENTITY(credentails);
        foreach (RpcAuthentication atype in authTypes)
        {
          RpcError result = RpcApi.RpcBindingSetAuthInfo(handle.Handle, serverPrincipalName, level, atype, ref pSecInfo, 0);
          if (result != RpcError.RPC_S_OK)
            Log.Warning("Unable to register {0}, result = {1}", atype, new RpcException(result).Message);
        }
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining | (MethodImplOptions)64 /* MethodImplOptions.NoOptimization undefined in 2.0 */)]
    private static byte[] InvokeRpc(RpcHandle handle, Guid iid, byte[] input)
    {
      Log.Verbose("InvokeRpc on {0}, sending {1} bytes", handle.Handle, input.Length);
      Ptr<MIDL_STUB_DESC> pStub;
      if (!handle.GetPtr(out pStub))
      {
        pStub =
            handle.CreatePtr(new MIDL_STUB_DESC(handle, handle.Pin(new RPC_CLIENT_INTERFACE(iid)),
                                                RpcApi.TYPE_FORMAT,
                                                false));
      }
      int szResponse = 0;
      var pszResponse = new Ptr<int>(szResponse);
      IntPtr response = IntPtr.Zero;
      var pResponse = new Ptr<IntPtr>(response);
           IntPtr result;
 
      using (Ptr<byte[]> pInputBuffer = new Ptr<byte[]>(input))
      {
        if (RpcApi.Is64BitProcess)
        {
          try
          {
            result = RpcApi.NdrClientCall2x64(
              pStub.Handle, RpcApi.FUNC_FORMAT_PTR.Handle, handle.Handle,
              input.Length, pInputBuffer.Handle, out szResponse, out response
              );
          }
          catch (SEHException ex)
          {
            int internalError = ex.ErrorCode;
            if ((uint)RpcError.RPC_E_FAIL == (uint)internalError)
            {
              if ((int)RpcError.RPC_S_OK != Marshal.GetExceptionCode())
              {
                internalError = Marshal.GetExceptionCode();
              }
            }
            RpcException.Assert(internalError);
            throw;
          }
        }
        else
        {
          using (Ptr<Int32[]> pStack32 = new Ptr<Int32[]>(new Int32[6]))
          {
            pStack32.Data[0] = handle.Handle.ToInt32();
            pStack32.Data[1] = input.Length;
            pStack32.Data[2] = pInputBuffer.Handle.ToInt32();
            pStack32.Data[3] = pszResponse.Handle.ToInt32();
            pStack32.Data[4] = pResponse.Handle.ToInt32();
            pStack32.Data[5] = 0; // reserved

            try
            {
              result = RpcApi.NdrClientCall2x86(pStub.Handle, RpcApi.FUNC_FORMAT_PTR.Handle, pStack32.Handle);
            }
            catch (SEHException ex)
            {
              int internalError = ex.ErrorCode;
              if ((uint)RpcError.RPC_E_FAIL == (uint)internalError)
              {
                if ((int)RpcError.RPC_S_OK != Marshal.GetExceptionCode())
                {
                  internalError = Marshal.GetExceptionCode();
                }
              }
              RpcException.Assert(internalError);
              throw;
            }
          }
        }
        GC.KeepAlive(pInputBuffer);
      }
      RpcException.Assert(result.ToInt32());

      szResponse = pszResponse.Data;
      response = pResponse.Data;
      byte[] output = new byte[szResponse];
      if (szResponse > 0 && pResponse.Handle != IntPtr.Zero)
      {
        Marshal.Copy(response, output, 0, output.Length);
      }
      RpcApi.Free(response);

      return output;
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void AsyncCallback(IntPtr pAsync, IntPtr pContext, RpcAsyncEvent _event);

    public delegate void APICallback(ExecuteAsyncResponse response);
    public delegate void AsyncRpcCallbackRountine(IntPtr response);

    [MethodImpl(MethodImplOptions.NoInlining | (MethodImplOptions)64 /* MethodImplOptions.NoOptimization undefined in 2.0 */)]
    private static void InvokeRpcAsync(RpcHandle handle, Guid iid, byte[] input, APICallback callback)
    {
      Log.Verbose("InvokeRpcAsync on {0}, sending {1} bytes", handle.Handle, input.Length);
      Ptr<MIDL_STUB_DESC> pStub;
      if (!handle.GetPtr(out pStub))
      {
        pStub = handle.CreatePtr(new MIDL_STUB_DESC(
              handle, handle.Pin(new RPC_CLIENT_INTERFACE(iid)), RpcApi.TYPE_FORMAT, false));
      }

      int cbOutput = 0;
      var pcbOutput = new Ptr<int>(cbOutput);
      IntPtr result = IntPtr.Zero;
      IntPtr output = IntPtr.Zero;
      var pOutput = new Ptr<IntPtr>(output);

      using (Ptr<byte[]> pInputBuffer = new Ptr<byte[]>(input))
      {
        AsyncCallback cbRountine = (IntPtr pAsync, IntPtr pContext, RpcAsyncEvent _event) =>
        {
          RPC_ASYNC_STATE async = (RPC_ASYNC_STATE)Marshal.PtrToStructure(pAsync, typeof(RPC_ASYNC_STATE));
          var myCallback = (APICallback)Marshal.GetDelegateForFunctionPointer(async.UserInfo, typeof(APICallback));

          RpcError hr = RpcApi.RpcAsyncCompleteCall(pAsync, ref result);
          if (RpcError.RPC_S_OK == hr)
          {
            if (RpcError.RPC_S_OK == (RpcError) result.ToInt32())
            {
              ExecuteAsyncResponse response;
              try
              {
                cbOutput = pcbOutput.Data;
                byte[] _output = new byte[cbOutput];
                Marshal.Copy(pOutput.Data, _output, 0, cbOutput);

                response.result = RpcError.RPC_S_OK;
                response.response = _output;
              }
              catch (Exception e)
              {
                response.result = RpcError.RPC_E_FAIL;
                response.response = null;
              }
              finally
              {
                RpcApi.Free(pOutput.Data);
              }

              myCallback(response);
            }
            else
            {
              ExecuteAsyncResponse response;
              response.result = (RpcError)result.ToInt32();
              response.response = null;
              myCallback(response);
            }
          }
          else
          {
            ExecuteAsyncResponse response;
            response.result = hr;
            response.response = null;
            myCallback(response);
          }
        };

        var pCallbackRoutine = new FunctionPtr<AsyncCallback>(cbRountine);

        RPC_ASYNC_STATE asyncState = new RPC_ASYNC_STATE();
        var _pAsyncState = new Ptr<RPC_ASYNC_STATE>(asyncState);
        RpcApi.RpcAsyncInitializeHandle(_pAsyncState.Handle, Marshal.SizeOf(typeof(RPC_ASYNC_STATE)));
        asyncState.Event = _pAsyncState.Data.Event;
        asyncState.Flags = _pAsyncState.Data.Flags;
        asyncState.Lock = _pAsyncState.Data.Lock;
        asyncState.RuntimeInfo = _pAsyncState.Data.RuntimeInfo;
        asyncState.Signature = _pAsyncState.Data.Signature;
        asyncState.Size = _pAsyncState.Data.Size;
        asyncState.StubInfo = _pAsyncState.Data.StubInfo;
        asyncState.NotificationType = RpcNotificationTypes.RpcNotificationTypeCallback;
        asyncState.u.NotificationRoutine = pCallbackRoutine.Handle;

        var ptrCB = Marshal.GetFunctionPointerForDelegate(callback);
        asyncState.UserInfo = ptrCB;

        var pAsyncState = new Ptr<RPC_ASYNC_STATE>(asyncState);

        if (RpcApi.Is64BitProcess)
        {
          try
          {
            result = RpcApi.NdrAsyncClientCallx64(pStub.Handle, RpcApi.ASYNC_FUNC_FORMAT_PTR.Handle, pAsyncState.Handle, handle.Handle,
              input.Length, pInputBuffer.Handle, out cbOutput, out output);
          }
          catch (SEHException ex)
          {
            RpcException.Assert(ex.ErrorCode);
            throw;
          }
        }
        else
        {
          using (Ptr<Int32[]> pStack32 = new Ptr<Int32[]>(new Int32[7]))
          {
            pStack32.Data[0] = pAsyncState.Handle.ToInt32();
            pStack32.Data[1] = handle.Handle.ToInt32();
            pStack32.Data[2] = input.Length;
            pStack32.Data[3] = pInputBuffer.Handle.ToInt32();
            pStack32.Data[4] = pcbOutput.Handle.ToInt32();
            pStack32.Data[5] = pOutput.Handle.ToInt32();
            pStack32.Data[6] = 0; //reserved

            try
            {
              result = RpcApi.NdrAsyncClientCallx86(pStub.Handle, RpcApi.ASYNC_FUNC_FORMAT_PTR.Handle, pStack32.Handle);
            }
            catch (SEHException ex)
            {
              Log.Verbose("exception on {0}", ex);
              RpcException.Assert(ex.ErrorCode);
              throw;
            }
          }
        }

        GC.KeepAlive(pInputBuffer);
      }
      RpcException.Assert(result.ToInt32());
      Log.Verbose("InvokeRpc.InvokeRpc response on {0}", handle.Handle);
    }

  }
}