namespace NugetDownloader.Core.Logging
{
  /// <summary>Null logger implementation.</summary>
  internal class NullLogger : ILogger
  {
    #region Public Methods

    /// <inheritdoc />
    public void Debug(string message)
    {
    }

    /// <inheritdoc />
    public void Info(string message)
    {
    }

    #endregion
  }
}
