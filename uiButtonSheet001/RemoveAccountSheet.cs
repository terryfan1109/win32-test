using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace uiButtonSheet001
{
  public partial class RemoveAccountSheet : Form
  {
    public event removeAccountDelegate removeAccount;
    public event removeCompleteActionDelegate removeCompleteAction;
    public event dataProviderChangedDelegate dataProviderChanged;

    private IEnumerable<AccountEntity> _dataProvider;

    public RemoveAccountSheet()
    {
      dataProviderChanged += onDataProviderChanged;

      InitializeComponent();
    }

    public IEnumerable<AccountEntity> dataProvider
    {
      get
      {
        return _dataProvider;
      }

      set
      {
        if (null != value && _dataProvider != value)
        {
          _dataProvider = value;
          if (null != dataProviderChanged) { dataProviderChanged(); }
        }
      }
    }
    
    private void RemoveAccountSheet_Load(object sender, EventArgs e)
    {
      onDataProviderChanged();
      CenterToScreen();
    }

    private void onDataProviderChanged()
    {
      flowLayoutPanel1.Controls.Clear();

      if (null != dataProvider)
      {
        foreach (var entity in dataProvider)
        {
          var btn = new Button();
          btn.Text = entity.displayName;
          btn.Width = 280;
          btn.Height = 53;
          btn.Margin = new Padding(0);
          btn.Click += accountBtn_Click;

          flowLayoutPanel1.Controls.Add(btn);
        }
      }
    }

    /**
     * UI control event handling
     */
    private void accountBtn_Click(object sender, EventArgs e)
    {
      if (null != removeAccount)
      {
        var btn = (Button)sender;
        removeAccount(btn.Text);
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (null != removeCompleteAction)
      {
        removeCompleteAction();
      }
    }
  
  }
}
