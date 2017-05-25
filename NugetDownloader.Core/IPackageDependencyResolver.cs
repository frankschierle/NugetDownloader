namespace NugetDownloader.Core
{
  using System;
  using System.Collections.Generic;
  using System.IO;

  /// <summary>Provides methods to resolve the dependencies of a package.</summary>
  public interface IPackageDependencyResolver
  {
    #region Public Methods

    /// <summary>Resolves all dependencies of a package.</summary>
    /// <param name="packageFileName">The path to the package to analyze.</param>
    /// <returns>A <see cref="PackageId"/> object for every dependency of the analyzed package.</returns>
    /// <exception cref="ArgumentNullException">Occurs if <paramref name="packageFileName"/> is null.</exception>
    /// <exception cref="FileNotFoundException">Occurs if <paramref name="packageFileName"/> does not exist.</exception>
    IEnumerable<PackageId> ResolveDirectDependencies(string packageFileName);

    #endregion
  }
}
