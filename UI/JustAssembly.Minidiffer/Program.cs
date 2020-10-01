using System;
using System.Windows.Forms;

namespace JustAssembly.Minidiffer
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main (string[] args)
    {
      Application.EnableVisualStyles ();
      Application.SetCompatibleTextRenderingDefault (false);
      Application.Run (new Form1(args?[0]));
    }
  }
}
