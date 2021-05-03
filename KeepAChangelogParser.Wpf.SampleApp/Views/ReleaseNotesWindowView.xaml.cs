using KeepAChangelogParser.Wpf.SampleApp.Contracts.ReleaseNotesWindow;
using System.Windows;

namespace KeepAChangelogParser.Wpf.SampleApp.Views
{

  /// <summary>
  /// Interaction logic for ReleaseNotesWindowView.xaml
  /// </summary>
  public partial class ReleaseNotesWindowView :
    Window,
    IReleaseNotesWindowView
  {

    public ReleaseNotesWindowView()
    {
      this.InitializeComponent();
    }

  }

}
