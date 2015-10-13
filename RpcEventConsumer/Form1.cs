using System;
using System.IO;
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
using Rpc.Event.Protocol;
using ProtoBuf;

namespace RpcEventConsumer
{
  public partial class Form1 : Form
  {
    private bool bSubscribed = false;
    private int number = 0;
    RpcClientApi client;

    public Form1()
    {
      InitializeComponent();

      var iid = new Guid("{04f5e707-d969-4822-95c5-2f3d4fd47f77}");
      client = new RpcClientApi(iid, RpcProtseq.ncalrpc, null, "3242");
      client.AuthenticateAs(RpcClientApi.Self);
      subscribeEvent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      subscribeEvent();

      var request = new Rpc.Event.Protocol.Request() {
        value = ++number
      };

      try
      {
        client.Execute(request.toByteArray());
      }
      catch (RpcException ex)
      {
        if (RpcError.RPC_S_SERVER_UNAVAILABLE == ex.RpcError || RpcError.RPC_S_SERVER_TOO_BUSY == ex.RpcError)
        {
          textBox1.AppendText(String.Format("RPC Server Unavailable, Error={0}\r\n", ex));
        }
        else
        {
          throw;
        }        
      }
    }

    private void subscribeEvent()
    {
      if (!bSubscribed)
      {
        var request = new Rpc.Event.Protocol.EventRequest();

        try
        {
          client.ExecuteAsync(request.toByteArray()).
            ContinueWith(r =>
            {
              this.InvokeIfRequired(() => onEvent(r.Result));
            });
        }
        catch (RpcException ex)
        {
          if (RpcError.RPC_S_SERVER_UNAVAILABLE == ex.RpcError || RpcError.RPC_S_SERVER_TOO_BUSY == ex.RpcError)
          {
            textBox1.AppendText(String.Format("RPC Server Unavailable, Error={0}\r\n", ex));
          }
          else
          {
            throw;
          }
        }

        bSubscribed = true;
      }
    }

    void onEvent(RpcClientApi.ExecuteAsyncResponse response)
    {
      bSubscribed = false;

      textBox1.AppendText(String.Format("{0} Event received. result={1}, response={2}\r\n",
        DateTime.UtcNow, response.result, response.response));

      if (response.result != RpcError.RPC_S_SERVER_UNAVAILABLE)
      {
        subscribeEvent();
      }
    }
  }

  public static class ControlExtensionHelp
  {
    public static void InvokeIfRequired(this Control control, MethodInvoker action)
    {
      if (control.InvokeRequired)
      {
        control.Invoke(action);
      }
      else
      {
        action();
      }
    }
  }

}
