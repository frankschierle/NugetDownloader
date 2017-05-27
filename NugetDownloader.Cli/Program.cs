namespace NugetDownloader.Cli
{
  using System;
  using System.Diagnostics;

  /// <summary>Provides the application main entry point.</summary>
  public static class Program
  {
    #region Public Methods

    /// <summary>Application main entry point.</summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args)
    {
      CommandLineOptions options;

      if (CommandLineParser.Parse(args, out options))
      {
      }
      else
      {
        PrintUsage();
        Environment.Exit(1);
      }

      Environment.Exit(0);
    }

    #endregion

    #region Private Methods

    /// <summary>Print out usage instructions to the console.</summary>
    private static void PrintUsage()
    {
      string processName = Process.GetCurrentProcess().ProcessName;

      Console.WriteLine("Usage: {0} DownloadTargetDirectory NuGetPackageId [NuGetPackageVersion]\n", processName);
      Console.WriteLine("Examples:");
      Console.WriteLine("\tDownload the latest version of NuGet package Microsoft.CodeAnalysis\n\tto directory C:\\Downloads:");
      Console.WriteLine("\t\t{0} \"C:\\Downloads\" Microsoft.CodeAnalysis", processName);
      Console.WriteLine("\n\tDownload a specific version of NuGet package Microsoft.CodeAnalysis\n\tto directory C:\\Downloads:");
      Console.WriteLine("\t\t{0} \"C:\\Downloads\" Microsoft.CodeAnalysis 2.1.0", processName);
    }

    #endregion
  }
}
