using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RpcLibrary;
using RpcTransport;

namespace launchProcessFromServiceClient
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      RpcLibrary.Log.VerboseEnabled = true;
      var iid = new Guid("{78323803-786f-4f7b-908d-b2e89c41d45f}");
      var client = new RpcClientApi(iid, RpcProtseq.ncalrpc, null, "11111");
      client.AuthenticateAs(RpcClientApi.Self);

      byte[] response = client.Execute(new byte[] { 1 });
    }
  }
}
