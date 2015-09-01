using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace libCSharpEcho
{
    [ComVisible(true), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("D94F4C9B-5C08-4566-B3DB-A3B6C543F485")]
    public interface CalculatorCallback
    {
        void callback(int result);
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void onEvent(int result);

    [ComVisible(true), InterfaceType(ComInterfaceType.InterfaceIsDual), Guid("A6193A2C-65F4-430D-A4EB-143D6B9A0EE4")]
    public interface Calculator
    {
        int calculate(String value);
        void asyncCalculate(CalculatorCallback callback, String value);
    }

    [ComVisible(true), ClassInterface(ClassInterfaceType.None), Guid("A77CB584-8C98-4463-B090-84418F1740AF")]
    public class CalculatorImpl : Calculator
    {
        public int calculate(String value)
        {
            return value.Length;
        }

        public void asyncCalculate(CalculatorCallback callback, String value)
        {
            callback.callback(value.Length);
        }
    }
}
