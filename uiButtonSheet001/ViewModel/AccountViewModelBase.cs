using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uiButtonSheet001.ViewModel
{
  public abstract class AccountViewModelBase: AccountViewModel
  {
    public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

    public abstract IEnumerable<AccountEntity> accounts
    {
      get;
    }

    public void addAccont()
    {
    }

    public void removeAccount(string account)
    {
    }
  }
}
