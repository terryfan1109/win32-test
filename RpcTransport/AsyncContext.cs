using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Runtime.InteropServices;
using RpcLibrary;
using RpcLibrary.Interop;

namespace RpcTransport
{
  public enum AsyncContextStatus
  {
    InProgress,
    Aborted,
    Completed,
    Canceled
  }

  /** asynchrouns request context
   * 
   */
  public interface AsyncContext
  {
    AsyncContextStatus status { get; }
    RpcError completeCall(uint exceptionCode);
    RpcError completeCall(byte[] output);
    RpcError completeCall(byte[] output, uint exceptionCode);
    RpcError abortCall(uint exceptionCode);
    RpcError testCancel();
  }

  /** implementation of asynchrouns request context
   * 
   */
  public abstract class AsyncContextBase : AsyncContext
  {
    protected abstract IntPtr asyncState { get; }
    public abstract AsyncContextStatus status
    {
      get;
      protected set;
    }
    protected abstract void putOutput(byte[] value);

    public RpcError completeCall(uint exceptionCode)
    {
      return completeCall(null, exceptionCode);
    }

    public RpcError completeCall(byte[] value)
    {
      return completeCall(value, 0);
    }

    public RpcError completeCall(byte[] value, uint exceptionCode)
    {
      if (AsyncContextStatus.InProgress != status)
      {
        throw new InvalidOperationException(String.Format("Invalid operation, the current state = {0}.", status));
      }

      if (null != value)
      {
        // write output payload
        putOutput(value);
      }

      var rt = (IntPtr) exceptionCode;
      RpcError result = RpcApi.RpcAsyncCompleteCall(asyncState, ref rt);
      status = AsyncContextStatus.Completed;
      return result;
    }

    public RpcError abortCall(uint exceptionCode)
    {
      if (AsyncContextStatus.InProgress != status)
      {
        throw new InvalidOperationException(String.Format("Invalid operation, the current state = {0}.", status));
      }
      RpcError result = RpcApi.RpcAsyncAbortCall(asyncState, exceptionCode);
      status = AsyncContextStatus.Aborted;
      return result;
    }

    public RpcError testCancel()
    {
      RpcError result = RpcApi.RpcServerTestCancel(RpcServerApi2.RpcAsyncGetCallHandle(asyncState));
      return result;
    }
  }

  /** concret class of asynchronous requet context
   * 
   */
  public class AsyncContextImpl : AsyncContextBase
  {
    private IntPtr _asyncState;
    private IntPtr _cbOutput;
    private IntPtr _output;
    private AsyncContextStatus _status;

    public AsyncContextImpl(IntPtr async, IntPtr cbOutput, IntPtr output)
    {
      _asyncState = async;
      _cbOutput = cbOutput;
      _output = output;
    }

    protected override IntPtr asyncState {
      get { return _asyncState; }
    }

    public override AsyncContextStatus status
    {
      get { return _status; }
      protected set { _status = value;  }
    }

    protected override void putOutput(byte[] value)
    {
      if (null == value) {
        throw new ArgumentException("output data coulnd not be null.");
      }
      if (0 == value.Length)
      {
        throw new ArgumentException("output data coulnd not be empty.");
      }

      var buffer = RpcApi.Alloc((uint) value.Length);
      Marshal.Copy(value, 0, buffer, value.Length);
      Marshal.WriteIntPtr(_output, buffer);
      Marshal.WriteInt32(_cbOutput, value.Length);
    }
  }
}
