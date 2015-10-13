using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;


namespace launchProcessByService
{
  public partial class Service1 : ServiceBase
  {
    MyService srv;

    public Service1()
    {
      InitializeComponent();
    }

    protected override void OnStart(string[] args)
    {
      srv = new MyService();
      srv.active("11111");
    }

    protected override void OnStop()
    {
      if (null != srv)
      {
        srv.deactive();
      }
    }
  }
}
