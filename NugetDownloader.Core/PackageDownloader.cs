namespace NugetDownloader.Core
{
  using System;
  using System.IO;
  using System.Net;

  /// <inheritdoc />
  public class PackageDownloader : IPackageDownloader
  {
    #region Private Fields

    /// <summary>The <see cref="IUrlFactory"/> used to create download URLs.</summary>
    private readonly IUrlFactory urlFactory;

    #endregion

    #region Public Constructors

    /// <summary>Initialises a new instance of the <see cref="PackageDownloader"/> class.</summary>
    /// <param name="urlFactory">The <see cref="IUrlFactory"/> to create download URLs.</param>
    /// <exception cref="ArgumentNullException">Occurs if <paramref name="urlFactory"/> is null.</exception>
    public PackageDownloader(IUrlFactory urlFactory)
    {
      if (urlFactory == null)
      {
        throw new ArgumentNullException(nameof(urlFactory));
      }

      this.urlFactory = urlFactory;
    }

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void DownloadPackage(PackageId package, string targetFileName)
    {
      Uri downloadUrl;

      if (package == null)
      {
        throw new ArgumentNullException(nameof(package));
      }

      if (targetFileName == null)
      {
        throw new ArgumentNullException(nameof(targetFileName));
      }

      if (File.Exists(targetFileName))
      {
        throw new IOException("Target file \"" + targetFileName + "\" already exists.");
      }

      downloadUrl = this.urlFactory.CreateDownloadUrl(package);

      using (var webClient = new WebClient())
      {
        webClient.DownloadFile(downloadUrl, targetFileName);
      }
    }

    #endregion
  }
}
