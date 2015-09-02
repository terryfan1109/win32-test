using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace libCSharpEcho
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void eventDelegate(int result);

    [ComVisible(true), InterfaceType(ComInterfaceType.InterfaceIsIDispatch), Guid("CCF7FD24-6395-4885-9798-5E125587AD8F")]
    public interface CalculatorEvent 
    {
        [DispId(1)]
        void onEvent(int result);
    }

    [ComVisible(true), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("D94F4C9B-5C08-4566-B3DB-A3B6C543F485")]
    public interface CalculatorCallback
    {   
        void callback(int result);
    }

    [ComVisible(true), InterfaceType(ComInterfaceType.InterfaceIsDual), Guid("A6193A2C-65F4-430D-A4EB-143D6B9A0EE4")]
    public interface Calculator
    {
        int calculate(String value);
        void asyncCalculate(CalculatorCallback callback, String value);
    }

    [ComVisible(true), ClassInterface(ClassInterfaceType.None), Guid("A77CB584-8C98-4463-B090-84418F1740AF")]
    [ComSourceInterfaces(typeof(CalculatorEvent))]
    public class CalculatorImpl : Calculator
    {
        public event eventDelegate onEvent;

        public int calculate(String value)
        {
            if (null != onEvent)
            {
                onEvent(1);
            }

            return value.Length;
        }

        public void asyncCalculate(CalculatorCallback callback, String value)
        {
            if (null != onEvent)
            {
                onEvent(2);
            }

            callback.callback(value.Length);
        }
    }
}
