using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uiButtonSheet001.ViewModel
{
  public interface AccountViewModel: System.ComponentModel.INotifyPropertyChanged
  {
    IEnumerable<AccountEntity> accounts { get; }

    void addAccont();
    void removeAccount(String account);
  }
}
