namespace NugetDownloader.Cli
{
  using System;
  using System.IO;
  using System.Linq;

  /// <summary>A simple command line parser.</summary>
  internal static class CommandLineParser
  {
    #region Public Methods

    /// <summary>Parse command line options.</summary>
    /// <param name="commandLineArguments">The command line arguments to parse.</param>
    /// <param name="options">The object to store the command line options to.</param>
    /// <returns>true if the command line arguments are valid. Otherwise false.</returns>
    /// <exception cref="ArgumentNullException">Occurs if <paramref name="commandLineArguments"/> is null.</exception>
    public static bool Parse(string[] commandLineArguments, out CommandLineOptions options)
    {
      CommandLineOptions cmdOptions = null;
      bool argsAreValid = false;
      string packageVersion;

      if (commandLineArguments == null)
      {
        throw new ArgumentNullException(nameof(commandLineArguments));
      }

      if ((commandLineArguments.Length >= 2) && (commandLineArguments.Length <= 3))
      {
        if (Path.GetInvalidFileNameChars().Any(commandLineArguments[0].Contains))
        {
          packageVersion = commandLineArguments.Length > 2 ? commandLineArguments[2] : null;
          cmdOptions = new CommandLineOptions(commandLineArguments[0], commandLineArguments[1], packageVersion);
          argsAreValid = true;
        }
      }

      options = cmdOptions;

      return argsAreValid;
    }

    #endregion
  }
}
