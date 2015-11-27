using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using Essential.Diagnostics;

namespace Logging.Demo.FileListener
{
  class ExecutionState
  {
    public int taskId { get; set; }
    public EventWaitHandle waitEvent { get; set; }
  }

  class Program
  {
    private static Random prng = new Random();
    private static TraceSource logger = new TraceSource("test");

    static void Main(string[] args)
    {
      var eventCollection = new List<EventWaitHandle>();

      for (int i = 0; i < 10; ++i)
      {
        var waitEvent = new ManualResetEvent(false);
        var state = new ExecutionState
        {
          taskId = i,
          waitEvent = waitEvent
        };

        ThreadPool.QueueUserWorkItem(new WaitCallback(Execute), state);
        eventCollection.Add(waitEvent);
      }

      EventWaitHandle.WaitAll(eventCollection.ToArray());

      Thread.Sleep(10 * 1000);
    }

    static void Execute(object state)
    {
      var executionState = (ExecutionState)state;

      using (var activity = new ActivityScope(logger))
      {
        Thread.Sleep(prng.Next() % 1000);
        logger.TraceEvent(TraceEventType.Error, 1, "message {0}", executionState.taskId);
        ExecuteSub(state);
        Thread.Sleep(prng.Next() % 1000);
        logger.TraceEvent(TraceEventType.Verbose, 1, "verbose {0}", executionState.taskId);

        if (null != executionState)
        {
          ((EventWaitHandle)executionState.waitEvent).Set();
        }
      }
    }

    static void ExecuteSub(object state)
    {
      var executionState = (ExecutionState)state;

      Thread.Sleep(prng.Next() % 1000);
      logger.TraceEvent(TraceEventType.Information, 1, "ExecuteSub: info {0}", executionState.taskId);

      var subInstance = new SubModule.SubClass();
      subInstance.method(executionState.taskId);
    }
  }
}
