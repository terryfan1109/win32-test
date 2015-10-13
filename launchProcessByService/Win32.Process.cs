using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Win32NS
{
  static public class Win32
  {
    public const int TOKEN_READ = 0x00020008; //From VC\PlatformSDK\Include\Winnt.h
    public const int TOKEN_IMPERSONATE = 0x0004;
    public const int ERROR_NO_TOKEN = 1008; //From VC\PlatformSDK\Include\WinError.h

    public enum LogonType
    {
      LOGON32_LOGON_INTERACTIVE = 2,
      LOGON32_LOGON_NETWORK = 3,
      LOGON32_LOGON_BATCH = 4,
      LOGON32_LOGON_SERVICE = 5,
      LOGON32_LOGON_UNLOCK = 7,
      LOGON32_LOGON_NETWORK_CLEARTEXT = 8,
      LOGON32_LOGON_NEW_CREDENTIALS = 9
    }

    [Flags]
    public enum LogonProvider
    {
      LOGON32_PROVIDER_DEFAULT = 0,
      LOGON32_PROVIDER_WINNT35,
      LOGON32_PROVIDER_WINNT40,
      LOGON32_PROVIDER_WINNT50
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct STARTUPINFO
    {
      public Int32 cb;
      public String lpReserved;
      public String lpDesktop;
      public String lpTitle;
      public Int32 dwX;
      public Int32 dwY;
      public Int32 dwXSize;
      public Int32 dwYSize;
      public Int32 dwXCountChars;
      public Int32 dwYCountChars;
      public Int32 dwFillAttribute;
      public Int32 dwFlags;
      public Int16 wShowWindow;
      public Int16 cbReserved2;
      public IntPtr lpReserved2;
      public IntPtr hStdInput;
      public IntPtr hStdOutput;
      public IntPtr hStdError;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PROCESS_INFORMATION
    {
      public IntPtr hProcess;
      public IntPtr hThread;
      public Int32 dwProcessId;
      public Int32 dwThreadId;
    }

    [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern Boolean LogonUser
    (
        String lpszUserName,
        String lpszDomain,
        String lpszPassword,
        LogonType dwLogonType,
        LogonProvider dwLogonProvider,
        out IntPtr phToken
    );

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern bool CreateProcessAsUser(
        IntPtr hToken,
        string lpApplicationName,
        string lpCommandLine,
        ref SECURITY_ATTRIBUTES lpProcessAttributes,
        ref SECURITY_ATTRIBUTES lpThreadAttributes,
        bool bInheritHandles,
        uint dwCreationFlags,
        IntPtr lpEnvironment,
        string lpCurrentDirectory,
        ref STARTUPINFO lpStartupInfo,
        out PROCESS_INFORMATION lpProcessInformation);

    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern Boolean CreateProcessWithLogonW
    (
        String lpszUsername,
        String lpszDomain,
        String lpszPassword,
        Int32 dwLogonFlags,
        String applicationName,
        String commandLine,
        Int32 creationFlags,
        IntPtr environment,
        String currentDirectory,
        ref STARTUPINFO sui,
        out PROCESS_INFORMATION processInfo
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern UInt32 WaitForSingleObject
    (
        IntPtr hHandle,
        UInt32 dwMilliseconds
    );

    [DllImport("kernel32", SetLastError = true)]
    public static extern Boolean CloseHandle(IntPtr handle);

    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool OpenThreadToken(
      IntPtr ThreadHandle,
      uint DesiredAccess,
      bool OpenAsSelf,
      out IntPtr TokenHandle);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetCurrentThread();

    [StructLayout(LayoutKind.Sequential)]
    public struct SECURITY_ATTRIBUTES
    {
      public int nLength;
      public IntPtr lpSecurityDescriptor;
      public int bInheritHandle;
    }

    [DllImport("kernel32.dll")]
    public static extern uint GetLastError();
  }
}
