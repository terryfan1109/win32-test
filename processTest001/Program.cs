using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace processTest001
{
  class Program
  {
    static void Main(string[] args)
    {
      IntPtr hCurrentThread;
      IntPtr hToken;

      Win32.OpenThreadToken(Win32.GetCurrentThread(), Win32.TOKEN_READ | Win32.TOKEN_IMPERSONATE, false, out hToken);

      Win32.SECURITY_ATTRIBUTES saProcessAttributes = new Win32.SECURITY_ATTRIBUTES();
      Win32.SECURITY_ATTRIBUTES saThreadAttributes = new Win32.SECURITY_ATTRIBUTES();

      Win32.STARTUPINFO startupInfo = new Win32.STARTUPINFO() ;
      startupInfo.cb = Marshal.SizeOf(startupInfo);

      Win32.PROCESS_INFORMATION processInfo = new Win32.PROCESS_INFORMATION();

      bool result = Win32.CreateProcessAsUser(
        hToken, @"c:\windows\notepad.exe", null,
        ref saProcessAttributes, ref saThreadAttributes, true, 0, IntPtr.Zero, null, ref startupInfo, out processInfo);
    }
  }
}
