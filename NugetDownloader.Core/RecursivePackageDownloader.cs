namespace NugetDownloader.Core
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.IO;
  using System.Linq;
  using System.Text;

  /// <summary>Provides methods to download a NuGet
  /// package and all of its dependencies.</summary>
  public class RecursivePackageDownloader
  {
    #region Private Fields

    /// <summary>The <see cref="IPackageDownloader"/> used to download a single package.</summary>
    private readonly IPackageDownloader packageDownloader;

    /// <summary>The <see cref="IPackageDependencyResolver"/> used to resolve all dependencies of a package.</summary>
    private readonly IPackageDependencyResolver packageDependencyResolver;

    #endregion

    #region Public Constructors

    /// <summary>Initialises a new instance of the <see cref="RecursivePackageDownloader"/> class.</summary>
    /// <param name="packageDownloader">The <see cref="IPackageDownloader"/> used to download a single package.</param>
    /// <param name="packageDependencyResolver">The <see cref="IPackageDependencyResolver"/> used to resolve all dependencies of a package.</param>
    /// <exception cref="ArgumentNullException">Occurs if any of the parameters is null.</exception>
    public RecursivePackageDownloader(IPackageDownloader packageDownloader, IPackageDependencyResolver packageDependencyResolver)
    {
      if (packageDownloader == null)
      {
        throw new ArgumentNullException(nameof(packageDownloader));
      }

      if (packageDependencyResolver == null)
      {
        throw new ArgumentNullException(nameof(packageDependencyResolver));
      }

      this.packageDownloader = packageDownloader;
      this.packageDependencyResolver = packageDependencyResolver;
    }

    #endregion

    #region Public Methods

    /// <summary>Downloads a package and all of its dependencies to the given target directory.</summary>
    /// <param name="package">The package to download.</param>
    /// <param name="targetDirectory">The target directory all packages should be saved to.</param>
    /// <exception cref="ArgumentNullException">Occurs if any of the parameters is null.</exception>
    /// <exception cref="DirectoryNotFoundException">Occurs if <paramref name="targetDirectory"/> does not exist.</exception>
    public void DownloadPackageAndAllDependencies(PackageId package, string targetDirectory)
    {
      if (package == null)
      {
        throw new ArgumentNullException(nameof(package));
      }

      if (targetDirectory == null)
      {
        throw new ArgumentNullException(nameof(targetDirectory));
      }

      if (!Directory.Exists(targetDirectory))
      {
        throw new DirectoryNotFoundException("Target directory \"" + targetDirectory + "\" not found.");
      }

      this.DownloadPackageRecursive(package, new List<PackageId>(), targetDirectory);
    }

    #endregion

    #region Private Methods

    /// <summary>Creates the target file name for a package.</summary>
    /// <param name="package">The package.</param>
    /// <param name="targetDirectory">The target directory the package should be saved to.</param>
    /// <returns>The target file name for the given <paramref name="package"/>.</returns>
    private static string CreateTargetFileName(PackageId package, string targetDirectory)
    {
      var pathBuilder = new StringBuilder(Path.Combine(targetDirectory, package.Name));

      if (package.Version != null)
      {
        pathBuilder.AppendFormat(CultureInfo.InvariantCulture, "_{0}", package.Version);
      }

      pathBuilder.Append(".nupkg");

      return pathBuilder.ToString();
    }

    /// <summary>Downloads a package and all of its dependencies.</summary>
    /// <param name="package">The package to download.</param>
    /// <param name="packagesAlreadyDownloaded">A list of already downloaded packages.</param>
    /// <param name="targetDirectory">The target directory all packages should be saved to.</param>
    private void DownloadPackageRecursive(PackageId package, IList<PackageId> packagesAlreadyDownloaded, string targetDirectory)
    {
      string targetFileName = CreateTargetFileName(package, targetDirectory);
      IEnumerable<PackageId> dependencies;

      if (!packagesAlreadyDownloaded.Any(p => p.Equals(package)))
      {
        this.packageDownloader.DownloadPackage(package, targetFileName);
        packagesAlreadyDownloaded.Add(package);

        dependencies = this.packageDependencyResolver.ResolveDirectDependencies(targetFileName);
        foreach (var dependency in dependencies)
        {
          this.DownloadPackageRecursive(dependency, packagesAlreadyDownloaded, targetDirectory);
        }
      }
    }

    #endregion
  }
}
