using Ardalis.GuardClauses;
using KeepAChangelogParser.Wpf.SampleApp.Commands.ReleaseNotesWindow;
using KeepAChangelogParser.Wpf.SampleApp.Contracts;
using KeepAChangelogParser.Wpf.SampleApp.Contracts.ReleaseNotesWindow;
using KeepAChangelogParser.Wpf.SampleApp.Services;
using KeepAChangelogParser.Wpf.SampleApp.Services.ReleaseNotesWindow;
using KeepAChangelogParser.Wpf.SampleApp.ViewModels.ReleaseNotesWindow;
using KeepAChangelogParser.Wpf.SampleApp.Views;
using SimpleInjector;

namespace KeepAChangelogParser.Wpf.SampleApp.CompositionRoot
{
  public static class TimeStopperComposer
  {
    public static void ComposeTimeStopper(
      this Container container
    )
    {
      _ = Guard.Against.Null(container, nameof(container));

      container.Register(typeof(IReleaseNotesWindowService), typeof(ReleaseNotesWindowService), Lifestyle.Singleton);
      container.Register(typeof(IReleaseNotesWindowClosedEventCommand), typeof(ReleaseNotesWindowClosedEventCommand), Lifestyle.Singleton);
      container.Register(typeof(IReleaseNotesWindowClickHyperlinkCommand), typeof(ReleaseNotesWindowClickHyperlinkCommand), Lifestyle.Singleton);
      container.Register(typeof(IReleaseNotesWindowClickOkCommand), typeof(ReleaseNotesWindowClickOkCommand), Lifestyle.Singleton);
      container.Register(typeof(IReleaseNotesWindowView), typeof(ReleaseNotesWindowView), Lifestyle.Singleton);
      container.Register(typeof(IReleaseNotesWindowViewModel), typeof(ReleaseNotesWindowViewModel), Lifestyle.Singleton);

      container.Register(typeof(IChangelogService), typeof(ChangelogService), Lifestyle.Singleton);
      container.Register(typeof(IFileService), typeof(FileService), Lifestyle.Singleton);
    }
  }
}
