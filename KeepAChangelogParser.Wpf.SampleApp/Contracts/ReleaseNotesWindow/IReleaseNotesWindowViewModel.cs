using System.Collections.ObjectModel;
using KeepAChangelogParser.Wpf.SampleApp.Models.ReleaseNotesWindow;

namespace KeepAChangelogParser.Wpf.SampleApp.Contracts.ReleaseNotesWindow
{
  public interface IReleaseNotesWindowViewModel
  {
    public ObservableCollection<ReleaseNotesDetail> DataGridItemsSourceCollection { get; }

    public void OnPropertyChanged(string propertyName);
  }
}
