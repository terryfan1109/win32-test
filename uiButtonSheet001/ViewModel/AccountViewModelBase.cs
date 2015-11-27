using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace uiButtonSheet001.ViewModel
{
  public abstract class AccountViewModelBase : AccountViewModel
  {
    public event PropertyChangedEventHandler PropertyChanged;

    public abstract List<AccountEntity> accounts
    {
      get;
    }

    public void addAccont()
    {
    }

    public void removeAccount(AccountEntity account)
    {
      accounts.Remove(account);

      if (null != PropertyChanged)
      {
        PropertyChanged(null, null);
      }
    }

    public void execute(object sender, object parameter)
    {
      ((Command)sender).execute(parameter);
    }

    Command _addAccountCommand;
    public Command addAccountCommand
    {
      get
      {
        return _addAccountCommand ?? (_addAccountCommand = new AddAccountCommand(this));
      }
    }

    Command _removeAccountCommand;
    public Command removeAccountCommand
    {
      get
      {
        return _removeAccountCommand ?? (_removeAccountCommand = new RemoveAccountCommand(this));
      }
    }
  }

  public class AddAccountCommand : Command
  {
    AccountViewModel _viewModel;

    public AddAccountCommand(AccountViewModel viewModel)
    {
      _viewModel = viewModel;
    }

    public void execute(object arg)
    {
      _viewModel.addAccont();
    }
  }

  public class RemoveAccountCommand : Command
  {
    AccountViewModel _viewModel;

    public RemoveAccountCommand(AccountViewModel viewModel)
    {
      _viewModel = viewModel;
    }

    public void execute(object arg)
    {
      _viewModel.removeAccount((AccountEntity)arg);
    }
  }
}
