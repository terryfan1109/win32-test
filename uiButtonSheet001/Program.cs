using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uiButtonSheet001
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      var accounts = new List<AccountEntity>();
      accounts.Add(new AccountEntity()
      {
        type = "com.company.account",
        name = "11467110-A123-43E6-A332-19595AFC9E57",
        displayName = "Jack Dancer",
        avatar = "http://avata.company.com/avatar.png"        
      });
      accounts.Add(new AccountEntity()
      {
        type = "com.company.account",
        name = "1BB0F006-8166-4CEE-905C-3F7C8227D2E9",
        displayName = "Mary Player",
        avatar = "http://avata.company.com/avatar.png"
      });
      accounts.Add(new AccountEntity()
      {
        type = "com.company.account",
        name = "952FEEE4-9B83-48CD-BD51-A5C8D4DCDDB2",
        displayName = "Tom Cook",
        avatar = "http://avata.company.com/avatar.png"
      });

      var selectAccountSheet = new SelectAccountSheet();
      selectAccountSheet.dataProvider = accounts;

      Application.Run(selectAccountSheet);
    }
  }
}
