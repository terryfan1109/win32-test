using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace TAPTest
{
  class SampleWorkflow {
    public Task<int> executeAsync(int number)
    {
      return executeAsync(number, CancellationToken.None, null);
    }
    public Task<int> executeAsync(int number, CancellationToken cancellationToken)
    {
      return executeAsync(number, cancellationToken, null);
    }
    public Task<int> executeAsync(int number, IProgress<Double> progress)
    {
      return executeAsync(number, CancellationToken.None, progress);
    }
    public Task<int> executeAsync(int number, CancellationToken cancellationToken, IProgress<Double> progress)
    {
      int period = 100;
      int totalCount = number * (1000 / period);

      return Task.Factory.StartNew<int>(() => {
        cancellationToken.ThrowIfCancellationRequested();

        for (Int16 i = 0; i < totalCount; ++i)
        {
          if (cancellationToken.IsCancellationRequested) {
            cancellationToken.ThrowIfCancellationRequested();
          }
          Thread.Sleep(period);
          if (null != progress) {
            progress.Report(((double) i)/totalCount);
          }
        }
        if (null != progress)
        {
          progress.Report(100.0);
        }
        return number;
      });
    }
  }


  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void TestCompleteAsyncTask()
    {
      var task = new SampleWorkflow().executeAsync(3);
      task.Wait();
      Assert.AreEqual(task.Result, 3);
    }

    [TestMethod]
    [ExpectedException(typeof(OperationCanceledException), "Operation has been canceled.")]
    public void TestCancelAsyncTask()
    {
      var cancelTknSrc = new CancellationTokenSource();
      var task = new SampleWorkflow().executeAsync(3, cancelTknSrc.Token);
      cancelTknSrc.Cancel();
      try
      {
        task.Wait();
      }
      catch (AggregateException e)
      {
        throw e.InnerException;
      }
    }

    [TestMethod]
    [ExpectedException(typeof(OperationCanceledException), "Operation has been canceled.")]
    public void TestCancelAsyncTask2()
    {
      var cancelTknSrc = new CancellationTokenSource();
      var task = new SampleWorkflow().executeAsync(3, cancelTknSrc.Token);
      Thread.Sleep(2000);
      cancelTknSrc.Cancel();
      try
      {
        task.Wait();
      }
      catch (AggregateException e)
      {
        throw e.InnerException;
      }
    }

    [TestMethod]
    public void TestProgressOfAsyncTask2()
    {
      Double progressResult = .0;
      var progress = new Progress<Double>();
      progress.ProgressChanged += (s, e) => {
        progressResult = e;
      };

      var task = new SampleWorkflow().executeAsync(3, progress);
      task.Wait();

      Assert.AreEqual(3, task.Result);
      Assert.AreEqual(100.0, progressResult);
    }

  }
}
