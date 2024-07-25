using KeepAChangelogParser.Wpf.SampleApp.Commands;
using KeepAChangelogParser.Wpf.SampleApp.Contracts.ReleaseNotesWindow;
using KeepAChangelogParser.Wpf.SampleApp.Models.ReleaseNotesWindow;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace KeepAChangelogParser.Wpf.SampleApp.ViewModels.ReleaseNotesWindow
{
  public class ReleaseNotesWindowViewModel :
    BaseViewModel,
    IReleaseNotesWindowViewModel
  {
    private readonly IReleaseNotesWindowClosedEventCommand releaseNotesWindowClosedEventCommand;
    private readonly IReleaseNotesWindowClickHyperlinkCommand releaseNotesWindowClickHyperlinkCommand;
    private readonly IReleaseNotesWindowClickOkCommand releaseNotesWindowClickOkCommand;

    private ICommand? releaseNotesWindowClosedEventRelayCommand = null;
    private ICommand? releaseNotesWindowClickHyperlinkRelayCommand = null;
    private ICommand? releaseNotesWindowClickOkRelayCommand = null;

    public ObservableCollection<ReleaseNotesDetail> DataGridItemsSourceCollection { get; } =
      new ObservableCollection<ReleaseNotesDetail>();

    public ICommand ClosedEventCommand
    {
      get
      {
        this.releaseNotesWindowClosedEventRelayCommand ??=
            new RelayCommand<CancelEventArgs>(
              _ => this.releaseNotesWindowClosedEventCommand.ExecuteClosed(),
              _ => this.releaseNotesWindowClosedEventCommand.CanExecuteClosed()
          );

        return this.releaseNotesWindowClosedEventRelayCommand;
      }
    }

    public ICommand ClickHyperlinkCommand
    {
      get
      {
        this.releaseNotesWindowClickHyperlinkRelayCommand ??=
            new RelayCommand<object>(
              this.releaseNotesWindowClickHyperlinkCommand.ExecuteClick
          );

        return this.releaseNotesWindowClickHyperlinkRelayCommand;
      }
    }

    public ICommand ClickOkButtonCommand
    {
      get
      {
        this.releaseNotesWindowClickOkRelayCommand ??=
            new RelayCommand<IReleaseNotesWindowView>(
              _ => this.releaseNotesWindowClickOkCommand.ExecuteClick(this)
          );

        return this.releaseNotesWindowClickOkRelayCommand;
      }
    }

    public ReleaseNotesWindowViewModel(
      IReleaseNotesWindowClosedEventCommand releaseNotesWindowClosedEventCommand,
      IReleaseNotesWindowClickHyperlinkCommand releaseNotesWindowClickHyperlinkCommand,
      IReleaseNotesWindowClickOkCommand releaseNotesWindowClickOkCommand
    )
    {
      this.releaseNotesWindowClosedEventCommand = releaseNotesWindowClosedEventCommand;
      this.releaseNotesWindowClickHyperlinkCommand = releaseNotesWindowClickHyperlinkCommand;
      this.releaseNotesWindowClickOkCommand = releaseNotesWindowClickOkCommand;
    }
  }
}
