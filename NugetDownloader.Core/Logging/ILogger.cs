namespace NugetDownloader.Core.Logging
{
  /// <summary>Provides simple methods to log some information.</summary>
  public interface ILogger
  {
    #region Public Methods

    /// <summary>Logs some debug information.</summary>
    /// <param name="message">The log message.</param>
    void Debug(string message);

    /// <summary>Logs some information.</summary>
    /// <param name="message">The log message.</param>
    void Info(string message);

    #endregion
  }
}
