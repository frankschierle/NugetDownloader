namespace NugetDownloader.Core
{
  using System;
  using System.IO;

  /// <summary>Provides methods to download NuGet packages.</summary>
  public interface IPackageDownloader
  {
    #region Public Methods

    /// <summary>Downloads a Nuget package and saves it to the given target directory.</summary>
    /// <param name="package">The package to download.</param>
    /// <param name="targetFileName">The target file name to save the package to.</param>
    /// <exception cref="ArgumentNullException">Occurs if any of the parameters is null.</exception>
    /// <exception cref="IOException">Occurs if the <paramref name="targetFileName"/> already exist.</exception>
    void DownloadPackage(PackageId package, string targetFileName);

    #endregion
  }
}
