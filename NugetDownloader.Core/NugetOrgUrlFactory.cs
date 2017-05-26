namespace NugetDownloader.Core
{
  using System;
  using System.Globalization;
  using System.Text;

  /// <inheritdoc />
  public class NugetOrgUrlFactory : IUrlFactory
  {
    #region Private Fields

    /// <summary>The base download URL for NuGet packages.</summary>
    private const string DownloadBaseUrl = "https://www.nuget.org/api/v2/package/";

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public Uri CreateDownloadUrl(PackageId package)
    {
      var urlBuilder = new StringBuilder(DownloadBaseUrl);

      if (package == null)
      {
        throw new ArgumentNullException(nameof(package));
      }

      urlBuilder.Append(package.Name);

      if (package.Version != null)
      {
        urlBuilder.AppendFormat(CultureInfo.InvariantCulture, "/{0}", package.Version);
      }

      return new Uri(urlBuilder.ToString());
    }

    #endregion
  }
}
