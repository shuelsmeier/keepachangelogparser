using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using KeepAChangelogParser.Wpf.SampleApp.CompositionRoot;
using KeepAChangelogParser.Wpf.SampleApp.Contracts;
using KeepAChangelogParser.Wpf.SampleApp.Contracts.ReleaseNotesWindow;
using System.Windows;

namespace KeepAChangelogParser.Wpf.SampleApp.Services
{
  public class StartupService :
    IStartupService
  {
    public void Execute(
      StartupEventArgs startupEventArgs
    )
    {
      Guard.Against.Null(startupEventArgs, nameof(startupEventArgs));

      Result<OnStartupContext> onStartupContextResult =
        Result.Success(new OnStartupContext(startupEventArgs.Args));

      onStartupContextResult = initializeContainer(onStartupContextResult);

      onStartupContextResult = setReleaseNotesWindowDataContext(onStartupContextResult);

      onStartupContextResult = initializeReleaseNotesWindowViewModel(onStartupContextResult);
      onStartupContextResult = showReleaseNotesWindowViewModel(onStartupContextResult);

      if (onStartupContextResult.IsFailure)
      {
        MessageBox.Show(
          onStartupContextResult.Error,
          "Error",
          MessageBoxButton.OK,
          MessageBoxImage.Error,
          MessageBoxResult.OK,
          MessageBoxOptions.None);

        Application.Current.Shutdown();
      }
    }

    private static Result<OnStartupContext> setReleaseNotesWindowDataContext(
      Result<OnStartupContext> onStartupContextResult
    )
    {
      if (onStartupContextResult.IsFailure) { return onStartupContextResult; }

      IReleaseNotesWindowViewModel releaseNotesWindowViewModel =
        ComposerManager.Container.GetInstance<IReleaseNotesWindowViewModel>();

      IReleaseNotesWindowView releaseNotesWindow =
        ComposerManager.Container.GetInstance<IReleaseNotesWindowView>();

      releaseNotesWindow.DataContext = releaseNotesWindowViewModel;

      return onStartupContextResult;
    }

    private static Result<OnStartupContext> initializeReleaseNotesWindowViewModel(
      Result<OnStartupContext> onStartupContextResult
    )
    {
      if (onStartupContextResult.IsFailure) { return onStartupContextResult; }

      IReleaseNotesWindowService releaseNotesWindowService =
        ComposerManager.Container.GetInstance<IReleaseNotesWindowService>();

      Result initializeResult =
        releaseNotesWindowService.Initialize();

      if (initializeResult.IsFailure) { return Result.Failure<OnStartupContext>(initializeResult.Error); }

      return onStartupContextResult;
    }

    private static Result<OnStartupContext> showReleaseNotesWindowViewModel(
      Result<OnStartupContext> onStartupContextResult
    )
    {
      if (onStartupContextResult.IsFailure) { return onStartupContextResult; }

      IReleaseNotesWindowView releaseNotesWindowView =
        ComposerManager.Container.GetInstance<IReleaseNotesWindowView>();

      releaseNotesWindowView.ShowDialog();

      return onStartupContextResult;
    }

    private static Result<OnStartupContext> initializeContainer(
      Result<OnStartupContext> onStartupContextResult
    )
    {
      if (onStartupContextResult.IsFailure) { return onStartupContextResult; }

      ComposerManager.Compose();

      return onStartupContextResult;
    }

    private class OnStartupContext
    {
      public OnStartupContext(
        string[] startupEventArgs
      )
      {
        this.StartupEventArgs = startupEventArgs;
      }

      public string[] StartupEventArgs { get; }
    }
  }
}
