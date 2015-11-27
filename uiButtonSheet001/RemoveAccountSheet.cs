using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using uiButtonSheet001.ViewModel;

namespace uiButtonSheet001
{
  public partial class RemoveAccountSheet : Form
  {
    public event removeCompleteActionDelegate removeCompleteAction;

    private ListChangedEventHandler _listChangedHandler;
    private EventHandler _positionChangedHandler;
    private object _dataSource;
    private string _dataMember;
    private CurrencyManager _dataManager;

    private AccountViewModel _viewModel;

    public RemoveAccountSheet()
    {
      InitializeComponent();
    }

    public AccountViewModel viewModel
    {
      get
      {
        return _viewModel;
      }

      set
      {
        if (_viewModel != value)
        {
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
          btn.Click += accountBtn_Click;
          btn.Tag = new CommandPair { command = viewModel.removeAccountCommand, argument = elm };

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

    private void RemoveAccountSheet_Load(object sender, EventArgs e)
    {
      CenterToScreen();
    }

    private void accountBtn_Click(object sender, EventArgs e)
    {
      var btn = (Button)sender;
      var commandPair = (CommandPair)btn.Tag;
      _viewModel.execute(commandPair.command, commandPair.argument);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (null != removeCompleteAction)
      {
        removeCompleteAction();
      }
    }

    private void viewModel_PropertyChanged(object sender, EventArgs e)
    {
      updateAllData();
    }

  }

  public class CommandPair
  {
    public Command command { get; set; }
    public Object argument { get; set; }
  }

}
