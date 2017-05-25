namespace NugetDownloader.Core
{
  using System;
  using System.Diagnostics.CodeAnalysis;
  using System.Globalization;
  using System.IO;
  using System.Net;
  using System.Text;

  /// <inheritdoc />
  [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "No unmanaged resources to dispose.")]
  public class PackageDownloader : IPackageDownloader, IDisposable
  {
    #region Private Fields

    /// <summary>The base download URL for NuGet packages.</summary>
    private const string DownloadBaseUrl = "https://www.nuget.org/api/v2/package/";

    /// <summary>The web client used to download packages.</summary>
    private readonly WebClient webClient;

    /// <summary>A flag inicating whether the object is disposed.</summary>
    private bool disposed;

    #endregion

    #region Public Constructors

    /// <summary>Initialises a new instance of the <see cref="PackageDownloader"/> class.</summary>
    public PackageDownloader()
    {
      this.webClient = new WebClient();
    }

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void DownloadPackage(PackageId package, string targetFileName)
    {
      string downloadUrl;

      if (package == null)
      {
        throw new ArgumentNullException(nameof(package));
      }

      if (targetFileName == null)
      {
        throw new ArgumentNullException(nameof(targetFileName));
      }

      if (this.disposed)
      {
        throw new ObjectDisposedException(this.GetType().Name);
      }

      if (File.Exists(targetFileName))
      {
        throw new IOException("Target file \"" + targetFileName + "\" already exists.");
      }

      downloadUrl = GetDownloadUrl(package);
      this.webClient.DownloadFile(downloadUrl, targetFileName);
    }

    /// <inheritdoc />
    [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "No unmanaged resources to dispose.")]
    public void Dispose()
    {
      this.webClient.Dispose();
      this.disposed = true;
      GC.SuppressFinalize(this);
    }

    #endregion

    #region Private Methods

    /// <summary>Gets the download URL for a given <see cref="PackageId"/>.</summary>
    /// <param name="package">The package to get the download URL for.</param>
    /// <returns>The download URL for the given package.</returns>
    private static string GetDownloadUrl(PackageId package)
    {
      var urlBuilder = new StringBuilder(DownloadBaseUrl);

      urlBuilder.Append(package.Name);

      if (package.Version != null)
      {
        urlBuilder.AppendFormat(CultureInfo.InvariantCulture, "/{0}", package.Version);
      }

      return urlBuilder.ToString();
    }

    #endregion
  }
}
