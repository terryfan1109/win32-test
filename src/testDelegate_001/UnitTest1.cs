using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime;

namespace testDelegate_001
{


  [TestClass]
  public class UnitTest1
  {
    class ArgumentObject {
      public int Value {get; set;}
    }

    static Task<int> executeAsync(Object objectA, Object objectB)
    {
      return Task.Factory.StartNew<int>(() =>
      {
        Thread.Sleep(3000);
        return (((WeakReference)objectA).Target as ArgumentObject).Value +
          (((WeakReference)objectB).Target as ArgumentObject).Value;
      });
    }

    static Task<int> executeAsync2(int a, int b)
    {
      var objectA = new WeakReference(new ArgumentObject {Value = a});
      var objectB = new WeakReference(new ArgumentObject {Value = b});
      return executeAsync(objectA, objectB);
    }

    [TestMethod]
    public void TestDelegateWithArgument()
    {
      var task1 = executeAsync2(1, 2);
      //GC.Collect();
      task1.Wait();
      Assert.AreEqual(task1.Result, 3);
    }
  }
}
