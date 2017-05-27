namespace NugetDownloader.Cli
{
  using System;
  using System.Diagnostics;
  using System.IO;

  using NugetDownloader.Core;

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
        try
        {
          EnsureTargetDireactoryExists(options.TargetDirectory);
          DownloadPackage(new PackageId(options.PackageName, options.PackageVersion), options.TargetDirectory);
        }
        catch (Exception ex)
        {
          Console.WriteLine("An unexpected error occured:\n{0}\n{1}", ex.Message, ex.StackTrace);
        }
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

    /// <summary>Downloads a package and all of its dependencies.</summary>
    /// <param name="package">The package to download.</param>
    /// <param name="targetDirectory">The target directory the packages should be saved to.</param>
    private static void DownloadPackage(PackageId package, string targetDirectory)
    {
      var urlFactory = new NugetOrgUrlFactory();
      var packageDownloader = new PackageDownloader(urlFactory);
      var dependencyResolver = new PackageDependencyResolver();
      var recursivePackageDownloader = new RecursivePackageDownloader(packageDownloader, dependencyResolver);

      recursivePackageDownloader.DownloadPackageAndAllDependencies(package, targetDirectory);
    }

    /// <summary>Ensures that the specified target directory exists.</summary>
    /// <param name="targetDirectory">The target directory.</param>
    private static void EnsureTargetDireactoryExists(string targetDirectory)
    {
      if (!Directory.Exists(targetDirectory))
      {
        Directory.CreateDirectory(targetDirectory);
      }
    }

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
