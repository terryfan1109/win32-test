using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uiButtonSheet001.ViewModel
{
  public interface AccountViewModel: System.ComponentModel.INotifyPropertyChanged
  {
    List<AccountEntity> accounts { get; }

    void addAccont();
    void removeAccount(AccountEntity account);
    void execute(object sender, object argument);

    Command addAccountCommand { get; }
    Command removeAccountCommand { get; }
  }
}
