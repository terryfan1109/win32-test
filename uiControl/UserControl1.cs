using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;
using System.Runtime.InteropServices;


namespace uiControl
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void eventDelegate(int result);

    [ComVisible(true), InterfaceType(ComInterfaceType.InterfaceIsIDispatch), Guid("5E4A81D0-4914-4B15-B544-B170DD997F60")]
    public interface ConrolEvent
    {
        [DispId(1)]
        void onEvent(int result);
    }

    [ProgId("uiControl.UserControl1")]
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("F961A475-A53D-46D2-8E9A-E32E073F23EC")]
    public partial class UserControl1 : UserControl
    {
        public event eventDelegate onEvent;

        public UserControl1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            onEvent(checkBox1.Checked ? 1 : 2);
        }


        [ComRegisterFunction()]
        public static void RegisterClass(string key)
        {
            // Strip off HKEY_CLASSES_ROOT\ from the passed key as I don't need it
            StringBuilder sb = new StringBuilder(key);
            sb.Replace(@"HKEY_CLASSES_ROOT\", "");

            // Open the CLSID\{guid} key for write access
            RegistryKey k = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

            // And create the 'Control' key - this allows it to show up in 
            // the ActiveX control container 
            RegistryKey ctrl = k.CreateSubKey("Control");
            ctrl.Close();

            // Next create the CodeBase entry - needed if not string named and GACced.
            RegistryKey inprocServer32 = k.OpenSubKey("InprocServer32", true);
            inprocServer32.SetValue("CodeBase", Assembly.GetExecutingAssembly().CodeBase);
            inprocServer32.Close();

            // Finally close the main key
            k.Close();
        }
    }
}
