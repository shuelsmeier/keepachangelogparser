using Ardalis.GuardClauses;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using KeepAChangelogParser.Wpf.SampleApp.Contracts.ReleaseNotesWindow;

namespace KeepAChangelogParser.Wpf.SampleApp.Commands.ReleaseNotesWindow
{
  public class ReleaseNotesWindowClickHyperlinkCommand :
    IReleaseNotesWindowClickHyperlinkCommand
  {
    public void ExecuteClick(
      object? param
    )
    {
      Guard.Against.Null(param, nameof(param));

      string? url = param.ToString();

      Guard.Against.NullOrEmpty(url, nameof(url));

      openUrl(url);
    }

    private static void openUrl(
      string url
    )
    {
      try
      {
        Process.Start(url);
      }
      catch (Exception exception) when (
           exception is Win32Exception
        || exception is ObjectDisposedException
        || exception is FileNotFoundException
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

          Process.Start(
            new ProcessStartInfo(
              "cmd",
              $"/c start {url}")
            {
              CreateNoWindow = true
            });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
          Process.Start(
            "xdg-open",
            url);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
          Process.Start(
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
