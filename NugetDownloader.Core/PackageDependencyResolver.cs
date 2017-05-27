namespace NugetDownloader.Core
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.IO.Compression;
  using System.Linq;
  using System.Xml;

  /// <inheritdoc />
  public class PackageDependencyResolver : IPackageDependencyResolver
  {
    #region Public Methods

    /// <inheritdoc />
    public IEnumerable<PackageId> ResolveDirectDependencies(string packageFileName)
    {
      var dependencies = new List<PackageId>();
      var nuspecFile = new XmlDocument();
      ZipArchiveEntry nuspecEntry;
      XmlNodeList dependencyNodes;
      string packageName;
      string packageVersion;

      if (packageFileName == null)
      {
        throw new ArgumentNullException(nameof(packageFileName));
      }

      if (!File.Exists(packageFileName))
      {
        throw new FileNotFoundException("Package file \"" + packageFileName + "\" not found.");
      }

      using (var package = ZipFile.OpenRead(packageFileName))
      {
        nuspecEntry = GetNuspecZipArchiveEntry(package);

        using (var stream = nuspecEntry.Open())
        {
          nuspecFile.Load(stream);

          dependencyNodes = nuspecFile.SelectNodes("//*[local-name()='dependency']");
          foreach (XmlNode node in dependencyNodes)
          {
            packageName = node.Attributes.GetNamedItem("id")?.Value;
            packageVersion = node.Attributes.GetNamedItem("version")?.Value;

            dependencies.Add(new PackageId(packageName, packageVersion));
          }
        }
      }

      // Target frameworks are currently ignored.
      // => The same dependency can be included more than once if
      // required by several target frameworks.
      // => Use distinct to clean up the list of dependencies.
      return dependencies.Distinct();
    }

    #endregion

    #region Private Methods

    /// <summary>Gets the <see cref="ZipArchiveEntry"/> of the NUSPEC file.</summary>
    /// <param name="archive">The package zip archive.</param>
    /// <returns>The <see cref="ZipArchiveEntry"/> of the NUSPEC file.</returns>
    private static ZipArchiveEntry GetNuspecZipArchiveEntry(ZipArchive archive)
    {
      return archive.Entries.Single(e => e.FullName.EndsWith(".NUSPEC", StringComparison.OrdinalIgnoreCase));
    }

    #endregion
  }
}
