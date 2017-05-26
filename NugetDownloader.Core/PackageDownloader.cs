namespace NugetDownloader.Core
{
  using System;
  using System.Globalization;
  using System.IO;
  using System.Net;
  using System.Text;

  /// <inheritdoc />
  public class PackageDownloader : IPackageDownloader
  {
    #region Private Fields

    /// <summary>The base download URL for NuGet packages.</summary>
    private const string DownloadBaseUrl = "https://www.nuget.org/api/v2/package/";

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

      if (File.Exists(targetFileName))
      {
        throw new IOException("Target file \"" + targetFileName + "\" already exists.");
      }

      downloadUrl = GetDownloadUrl(package);

      using (var webClient = new WebClient())
      {
        webClient.DownloadFile(downloadUrl, targetFileName);
      }
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
