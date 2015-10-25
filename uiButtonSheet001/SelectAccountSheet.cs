using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uiButtonSheet001.ViewModel;

namespace uiButtonSheet001
{
  public delegate void addActionDelegate();
  public delegate void removeActionDelegate();
  public delegate void removeCompleteActionDelegate();
  public delegate void selectAccountDelegate(String name);
  public delegate void removeAccountDelegate(String name);
  public delegate void exitActionDelegate();
  public delegate void dataProviderChangedDelegate();

  public partial class SelectAccountSheet : Form
  {
    public event addActionDelegate addAction;
    public event removeActionDelegate removeAction;
    public event removeCompleteActionDelegate removeCompleteAction;
    public event selectAccountDelegate selectAccount;
    public event removeAccountDelegate removeAccount;
    public event exitActionDelegate exitAction;
    public event dataProviderChangedDelegate dataProviderChanged;

    private IEnumerable<AccountEntity> _dataProvider;
    private RemoveAccountSheet _removeAccountSheet;

    public SelectAccountSheet()
    {
      addAction = onAddAction;
      removeAction = onRemoveAction;
      selectAccount = onSelectAccount;
      exitAction = onExitAction;

      _removeAccountSheet = new RemoveAccountSheet();
      _removeAccountSheet.removeCompleteAction += onRemoveCompleteAction;
      _removeAccountSheet.removeAccount += onRemoveAccount;

      dataProviderChanged += onDataProviderChanged;

      InitializeComponent();
    }

    public AccountViewModel viewModel
    {
      get;
      set;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      if (null != dataProviderChanged)
      {
        dataProviderChanged();
      }

      CenterToScreen();
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
          _removeAccountSheet.dataProvider = _dataProvider;
        }
      }
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
          btn.Click += accountButtons_Click;

          flowLayoutPanel1.Controls.Add(btn);
        }
      }
    }

    private void accountButtons_Click(object sender, EventArgs e)
    {
      if (null != selectAccount)
      {
        var btn = (Button)sender;
        selectAccount(btn.Text);
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (null != addAction)
      {
        addAction();
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (null != exitAction)
      {
        exitAction();
      }
    }

    private void button3_Click(object sender, EventArgs e)
    {
      if (null != removeAction)
      {
        removeAction();
      }
    }

    private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
    {
    }

    private void onAddAction()
    {
      MessageBox.Show("Add account");
    }

    private void onRemoveAction()
    {
      _removeAccountSheet.Show();
      Hide();
    }

    private void onRemoveCompleteAction()
    {
      Show();
      _removeAccountSheet.Hide();
    }

    private void onSelectAccount(String name)
    {
      MessageBox.Show(name);
    }

    private void onRemoveAccount(String name)
    {
      MessageBox.Show(name);
    }

    private void onExitAction()
    {
      Application.Exit();
    }    
  }

  public class AccountEntity
  {
    public String type { get; set; }
    public String name { get; set; }
    public String displayName { get; set; }
    public String avatar { get; set; }
  };


}
