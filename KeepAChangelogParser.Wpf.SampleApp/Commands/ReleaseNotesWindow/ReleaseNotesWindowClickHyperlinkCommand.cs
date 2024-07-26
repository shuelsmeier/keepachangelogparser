using Ardalis.GuardClauses;
using KeepAChangelogParser.Wpf.SampleApp.Contracts.ReleaseNotesWindow;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace KeepAChangelogParser.Wpf.SampleApp.Commands.ReleaseNotesWindow
{
  public class ReleaseNotesWindowClickHyperlinkCommand :
    IReleaseNotesWindowClickHyperlinkCommand
  {
    public void ExecuteClick(
      object? param
    )
    {
      _ = Guard.Against.Null(param, nameof(param));

      string? url = param.ToString();

      _ = Guard.Against.NullOrEmpty(url, nameof(url));

      openUrl(url);
    }

    private static void openUrl(
      string url
    )
    {
      try
      {
        _ = Process.Start(url);
      }
      catch (Exception exception)
      when (exception
        is Win32Exception
        or ObjectDisposedException
        or FileNotFoundException
      )
      {
        // Hack because of this: https://github.com/dotnet/corefx/issues/10361
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
          url =
            url.Replace(
              "&",
              "^&",
              StringComparison.Ordinal);

          _ = Process.Start(
            new ProcessStartInfo(
              "cmd",
              $"/c start {url}")
            {
              CreateNoWindow = true
            });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
          _ = Process.Start(
            "xdg-open",
            url);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
          _ = Process.Start(
            "open",
            url);
        }
        else
        {
          ExceptionDispatchInfo.Throw(
            exception);
        }
      }
    }
  }
}
