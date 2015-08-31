using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace libCSharpEcho
{
    [ComVisible(true), InterfaceType(ComInterfaceType.InterfaceIsDual), Guid("A6193A2C-65F4-430D-A4EB-143D6B9A0EE4")]
    public interface Echo
    {
        String echo(String value);
    }

    [ComVisible(true), ClassInterface(ClassInterfaceType.None), Guid("A77CB584-8C98-4463-B090-84418F1740AF")]
    public class EchoImpl : Echo
    {
        public String echo(String value) { return value; }
    }
}
