using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uiButtonSheet001.ViewModel
{
  public class AccountViewModelImpl : AccountViewModelBase
  {
    private List<AccountEntity> _accounts = new List<AccountEntity>();

    public AccountViewModelImpl ()
    {
      _accounts.Add(
        new AccountEntity() { type = "com.company.account", name = "498E7689-01A1-48FA-BB70-68E4D5432A0E", displayName = "Jack Dancer", avatar = "http://www.company.com/profile.gif" });
      _accounts.Add(
        new AccountEntity() { type = "com.company.account", name = "4152B425-474B-497B-88EE-3557F8EAAF5B", displayName = "Tom Wu", avatar = "http://www.company.com/profile.gif" });
      _accounts.Add(
        new AccountEntity() { type = "com.company.account", name = "7B710991-0EB3-42D3-AE89-5C49474532EB", displayName = "Mary Player", avatar = "http://www.company.com/profile.gif" });
      _accounts.Add(
        new AccountEntity() { type = "com.company.account", name = "2AA6A38B-6492-4E09-A2B6-02A934846F3B", displayName = "Su Fan", avatar = "http://www.company.com/profile.gif" });
    }

    public override IEnumerable<AccountEntity> accounts
    {
      get { return accounts; }
    }

  }
}
