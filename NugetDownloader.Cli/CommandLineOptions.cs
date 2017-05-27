namespace NugetDownloader.Cli
{
  /// <summary>Encapsulates command line options.</summary>
  internal class CommandLineOptions
  {
    #region Public Constructors

    /// <summary>Initialises a new instance of the <see cref="CommandLineOptions"/> class.</summary>
    /// <param name="targetDirectory">The target directory to save the downloaded packages to.</param>
    /// <param name="packageName">The name of the package to download.</param>
    /// <param name="packageVersion">The version of the package to download. Can be null.</param>
    public CommandLineOptions(string targetDirectory, string packageName, string packageVersion)
    {
      this.TargetDirectory = targetDirectory;
      this.PackageName = packageName;
      this.PackageVersion = packageVersion;
    }

    #endregion

    #region Public Properties

    /// <summary>Gets the target directory to save the downloaded packages to.</summary>
    public string TargetDirectory { get; }

    /// <summary>Gets the name of the package to download.</summary>
    public string PackageName { get; }

    /// <summary>Gets the version of the package to download. Can be null.</summary>
    public string PackageVersion { get; }

    #endregion
  }
}
