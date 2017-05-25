namespace NugetDownloader.Core
{
  using System;

  /// <summary>Encapsulates name and version of a package.</summary>
  public class PackageId
  {
    #region Public Constructors

    /// <summary>Initialises a new instance of the <see cref="PackageId"/> class.</summary>
    /// <param name="name">The name of the package.</param>
    /// <param name="version">The version of the package. Can be null.</param>
    public PackageId(string name, string version)
    {
      if (name == null)
      {
        throw new ArgumentNullException(nameof(name));
      }

      this.Name = name;
      this.Version = version;
    }

    #endregion

    #region Public Properties

    /// <summary>Get the name of the package.</summary>
    public string Name { get; }

    /// <summary>Gets the version of the package. Can be null.</summary>
    public string Version { get; }

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      PackageId idToCompare;
      bool equals;

      if ((obj == null) || (this.GetType() != obj.GetType()) || !ReferenceEquals(this, obj))
      {
        equals = false;
      }
      else
      {
        idToCompare = (PackageId)obj;
        equals = (this.Name == idToCompare.Name) && (this.Version == idToCompare.Version);
      }

      return equals;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      unchecked
      {
        int hash = -2128831035;
        int multiplier = 16777619;

        hash = (hash * multiplier) ^ this.Name.GetHashCode();
        hash = (hash * multiplier) ^ (!ReferenceEquals(null, this.Version) ? this.Version.GetHashCode() : 0);

        return hash;
      }
    }

    #endregion
  }
}
