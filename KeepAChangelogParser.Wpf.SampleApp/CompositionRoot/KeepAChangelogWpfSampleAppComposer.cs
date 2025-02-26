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

      container.Register<IReleaseNotesWindowService, ReleaseNotesWindowService>(Lifestyle.Singleton);
      container.Register<IReleaseNotesWindowClosedEventCommand, ReleaseNotesWindowClosedEventCommand>(Lifestyle.Singleton);
      container.Register<IReleaseNotesWindowClickHyperlinkCommand, ReleaseNotesWindowClickHyperlinkCommand>(Lifestyle.Singleton);
      container.Register<IReleaseNotesWindowClickOkCommand, ReleaseNotesWindowClickOkCommand>(Lifestyle.Singleton);
      container.Register<IReleaseNotesWindowView, ReleaseNotesWindowView>(Lifestyle.Singleton);
      container.Register<IReleaseNotesWindowViewModel, ReleaseNotesWindowViewModel>(Lifestyle.Singleton);

      container.Register<IChangelogService, ChangelogService>(Lifestyle.Singleton);
      container.Register<IFileService, FileService>(Lifestyle.Singleton);
    }
  }
}
