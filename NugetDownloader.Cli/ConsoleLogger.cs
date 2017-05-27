namespace NugetDownloader.Cli
{
  using System;

  using NugetDownloader.Core.Logging;

  /// <summary>A simple console logger.</summary>
  internal class ConsoleLogger : ILogger
  {
    #region Public Methods

    /// <inheritdoc />
    public void Debug(string message)
    {
      ConsoleColor colorBackup = Console.ForegroundColor;

      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine(message);
      Console.ForegroundColor = colorBackup;
    }

    /// <inheritdoc />
    public void Info(string message)
    {
      Console.WriteLine(message);
    }

    #endregion
  }
}
