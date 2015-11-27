using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace Logging.Demo.SubModule
{
  class ExecutionState
  {
    public int taskId { get; set; }
    public EventWaitHandle waitEvent { get; set; }
  }

  public class SubClass
  {
    static private Random prng = new Random();
    static private TraceSource logger = new TraceSource("test");

    public void method(int taskId)
    {
      Thread.Sleep(prng.Next() % 1000);
      logger.TraceEvent(TraceEventType.Information, 1, "SubClass: info {0}", taskId);

      var eventCollection = new List<EventWaitHandle>();

      for (int i = 0; i < 3; ++i)
      {
        var waitEvent = new ManualResetEvent(false);
        var state = new ExecutionState
        {
          taskId = i,
          waitEvent = waitEvent
        };

        ThreadPool.QueueUserWorkItem(new WaitCallback(subMethod), state);
        eventCollection.Add(waitEvent);
      }

      EventWaitHandle.WaitAll(eventCollection.ToArray());
    }

    private void subMethod(object state)
    {
      var executionState = (ExecutionState)state;

      Thread.Sleep(prng.Next() % 1000);
      logger.TraceEvent(TraceEventType.Information, 1, "SubClass.subMethod: info {0}", executionState.taskId);

      if (null != executionState)
      {
        ((EventWaitHandle)executionState.waitEvent).Set();
      }
    }
  }
}
