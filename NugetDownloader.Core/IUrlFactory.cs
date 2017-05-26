namespace NugetDownloader.Core
{
  using System;

  /// <summary>Provides methods to create URLs.</summary>
  public interface IUrlFactory
  {
    #region Public Methods

    /// <summary>Creates the download URL for a NuGet package.</summary>
    /// <param name="package">The package to create the download URL for.</param>
    /// <returns>The download URL for the NuGet package.</returns>
    /// <exception cref="ArgumentNullException">Occurs if <paramref name="package"/> is null.</exception>
    Uri CreateDownloadUrl(PackageId package);

    #endregion
  }
}
