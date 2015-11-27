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

    private RemoveAccountSheet _removeAccountSheet;

    private ListChangedEventHandler _listChangedHandler;
    private EventHandler _positionChangedHandler;
    private object _dataSource;
    private string _dataMember;
    private CurrencyManager _dataManager;

    private AccountViewModel _viewModel;

    public SelectAccountSheet()
    {
      _listChangedHandler = new ListChangedEventHandler(dataManager_ListChanged);
      _positionChangedHandler = new EventHandler(dataManager_PositionChanged);

      addAction = onAddAction;
      removeAction = onRemoveAction;
      selectAccount = onSelectAccount;
      exitAction = onExitAction;

      _removeAccountSheet = new RemoveAccountSheet();
      _removeAccountSheet.removeCompleteAction += onRemoveCompleteAction;

      InitializeComponent();
    }

    public AccountViewModel viewModel
    {
      get
      {
        return _viewModel;
      }

      set {
        if (_viewModel != value) {
          if (null != _viewModel)
          {
            _viewModel.PropertyChanged -= viewModel_PropertyChanged;
          }

          _viewModel = value;

          if (null != _viewModel)
          {
            var ds = new BindingSource { DataSource = _viewModel.accounts };
            DataSource = ds;

            _viewModel.PropertyChanged += viewModel_PropertyChanged;
          }

          _removeAccountSheet.viewModel = _viewModel;            
        }
      }
    }

		/// </summary>
		[TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
		[Category("Data")]
		[Description("Data Source")]
		[DefaultValue(null)]
		public object DataSource
		{
			get
			{
				return _dataSource;
			}
			set
			{
				if (_dataSource != value)
				{
					_dataSource = value;
					tryDataBinding();
				}
			}
		}

    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design",
       "System.Drawing.Design.UITypeEditor, System.Drawing")]
    [Description("Data Member")]
    [DefaultValue(null)]
    public string DataMember
    {
      get
      {
        return _dataMember;
      }
      set
      {
        if (_dataMember != value)
        {
          _dataMember = value;
          tryDataBinding();
        }
      }
    }

    protected CurrencyManager DataManager
    {
      get
      {
        return _dataManager;
      }
    }

    /// <summary>
    /// Renew the Databinding. BindingContext is changed, if you change the Parent.
    /// </summary>
    protected override void OnBindingContextChanged(EventArgs e)
    {
      tryDataBinding();
      base.OnBindingContextChanged(e);
    }

    /// <summary>
    /// Tries to get a new CurrencyManager for new DataBinding
    /// </summary>
    private void tryDataBinding()
    {
      if (DataSource == null || base.BindingContext == null)
        return;

      CurrencyManager cm;
      try
      {
        cm = (CurrencyManager)base.BindingContext[DataSource, DataMember];
      }
      catch (System.ArgumentException)
      {
        // If no CurrencyManager was found
        return;
      }

      if (_dataManager != cm)
      {
        // Unwire the old CurrencyManager
        if (_dataManager != null)
        {
          _dataManager.ListChanged -= _listChangedHandler;
          _dataManager.PositionChanged -= _positionChangedHandler;
        }

        _dataManager = cm;
        // Wire the new CurrencyManager
        if (_dataManager != null)
        {
          _dataManager.ListChanged += _listChangedHandler;
          _dataManager.PositionChanged += _positionChangedHandler;
        }

        // Update metadata and data
        updateAllData();
      }
    }

    private void updateAllData()
    {
      flowLayoutPanel1.Controls.Clear();

      PropertyDescriptorCollection propColl = DataManager.GetItemProperties();

      foreach (var elm in DataManager.List)
      {
        PropertyDescriptor prop = propColl.Find("displayName", false);
        if (prop != null)
        {
          var btn = new Button();
          btn.Text = prop.GetValue(elm).ToString();
          btn.Width = 252;
          btn.Height = 50;
          btn.Margin = new Padding(0, 0, 0, 0);
          btn.Click += accountButtons_Click;

          flowLayoutPanel1.Controls.Add(btn);
        }
      }
    }

    private void dataManager_ListChanged(object sender, ListChangedEventArgs e)
    {
      if (e.ListChangedType == ListChangedType.Reset || e.ListChangedType == ListChangedType.ItemMoved)
      {
        // Update all data
        updateAllData();
      }
      else if (e.ListChangedType == ListChangedType.ItemAdded)
      {
        // Add new Item
        //addItem(e.NewIndex);
      }
      else if (e.ListChangedType == ListChangedType.ItemChanged)
      {
        // Change Item
        //updateItem(e.NewIndex);
      }
      else if (e.ListChangedType == ListChangedType.ItemDeleted)
      {
        // Delete Item
        //deleteItem(e.NewIndex);
      }
      else
      {
        // Update metadata and all data
        updateAllData();
      }
    }

    private void dataManager_PositionChanged(object sender, EventArgs e)
    {
      if (Container.Components.Count > DataManager.Position)
      {
        // TODO
      }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      CenterToScreen();
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

    private void viewModel_PropertyChanged(object sender, EventArgs e)
    {
      updateAllData();
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
