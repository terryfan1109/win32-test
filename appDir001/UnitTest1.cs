using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace appDir001
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void TestCreateAppDir()
    {
      string appBaseFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      string appFolder = Path.Combine(appBaseFolder, "myAppDir");
      if (!Directory.Exists(appFolder))
      {
        Directory.CreateDirectory(appFolder);
      }
    }
  }
}
