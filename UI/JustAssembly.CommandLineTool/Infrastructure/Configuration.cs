using System;
using System.IO;
using JustAssembly.API.Analytics;

namespace JustAssembly
{
  internal static class Configuration
  {
    private const string AppDirectoryName = "JustAssembly";

    public static IAnalyticsService Analytics { get; set; }

    public static string GetApplicationTempFolder
    {
      get { return Path.Combine (Path.GetTempPath(), AppDirectoryName); }
    }

    public static string JustAssemblyAppDataFolder
    {
      get
      {
        var userRoamingHomeDir = Environment.GetFolderPath (Environment.SpecialFolder.ApplicationData);
        var justAssemblyFolder = Path.Combine (userRoamingHomeDir, AppDirectoryName);
        EnsureDirectoryExists (justAssemblyFolder);

        return justAssemblyFolder;
      }
    }

    private static void EnsureDirectoryExists (string directoryName)
    {
      if (!Directory.Exists (directoryName))
      {
        Directory.CreateDirectory (directoryName);
      }
    }
  }
}