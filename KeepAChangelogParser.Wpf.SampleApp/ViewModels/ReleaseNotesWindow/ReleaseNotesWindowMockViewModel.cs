using KeepAChangelogParser.Models;
using KeepAChangelogParser.Wpf.SampleApp.Contracts.ReleaseNotesWindow;
using KeepAChangelogParser.Wpf.SampleApp.Models.ReleaseNotesWindow;
using System.Collections.ObjectModel;

namespace KeepAChangelogParser.Wpf.SampleApp.ViewModels.ReleaseNotesWindow
{

  public class ReleaseNotesWindowMockViewModel :
    IReleaseNotesWindowViewModel
  {

    public ObservableCollection<ReleaseNotesDetail> DataGridItemsSourceCollection
    {
      get
      {
        ObservableCollection<ReleaseNotesDetail>? releaseNotesDetailObservableCollection =
          new ObservableCollection<ReleaseNotesDetail>()
          {
            new ReleaseNotesDetail()
            {
              MarkdownTitle =
                "# Changelog",
              MarkdownText =
                "All notable changes to this project will be documented in this file. The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).",
            }
          };

        ReleaseNotesDetailSection? releaseNotesDetailSectionOne =
          new ReleaseNotesDetailSection()
          {
            MarkdownTitle =
              "2.0.0",
            MarkdownDate =
              "released on 2020-02-01",
          };

        releaseNotesDetailObservableCollection[0].
          SectionCollection.
            Add(releaseNotesDetailSectionOne);

        addReleaseNotesDetailSectionOneSubSection(ChangelogSubSectionType.Added, releaseNotesDetailObservableCollection);
        addReleaseNotesDetailSectionOneSubSection(ChangelogSubSectionType.Changed, releaseNotesDetailObservableCollection);
        addReleaseNotesDetailSectionOneSubSection(ChangelogSubSectionType.Deprecated, releaseNotesDetailObservableCollection);
        addReleaseNotesDetailSectionOneSubSection(ChangelogSubSectionType.Fixed, releaseNotesDetailObservableCollection);
        addReleaseNotesDetailSectionOneSubSection(ChangelogSubSectionType.Removed, releaseNotesDetailObservableCollection);
        addReleaseNotesDetailSectionOneSubSection(ChangelogSubSectionType.Security, releaseNotesDetailObservableCollection);

        ReleaseNotesDetailSection? releaseNotesDetailSectionTwo =
          new ReleaseNotesDetailSection()
          {
            MarkdownTitle =
              "1.0.0",
            MarkdownDate =
              "released on 2020-01-01",
          };

        releaseNotesDetailObservableCollection[0].
          SectionCollection.
            Add(releaseNotesDetailSectionTwo);

        addReleaseNotesDetailSectionTwoSubSection(ChangelogSubSectionType.Added, releaseNotesDetailObservableCollection);
        addReleaseNotesDetailSectionTwoSubSection(ChangelogSubSectionType.Added, releaseNotesDetailObservableCollection);
        addReleaseNotesDetailSectionTwoSubSection(ChangelogSubSectionType.Added, releaseNotesDetailObservableCollection);

        return releaseNotesDetailObservableCollection;
      }
    }

    private static void addReleaseNotesDetailSectionTwoSubSection(
      ChangelogSubSectionType changelogSubSectionType,
      ObservableCollection<ReleaseNotesDetail> releaseNotesDetailObservableCollection
    )
    {
      ReleaseNotesDetailSubSection releaseNotesDetailSectionTwoSubSection =
        new ReleaseNotesDetailSubSection()
        {
          Type =
            changelogSubSectionType,
          MarkdownTitle =
            changelogSubSectionType.ToString().ToUpper(),
          MarkdownText =
            "...",
        };

      releaseNotesDetailObservableCollection[0].
        SectionCollection[1].
          SubSectionCollection.
            Add(releaseNotesDetailSectionTwoSubSection);
    }

    private static void addReleaseNotesDetailSectionOneSubSection(
      ChangelogSubSectionType changelogSubSectionType,
      ObservableCollection<ReleaseNotesDetail> releaseNotesDetailObservableCollection
    )
    {
      ReleaseNotesDetailSubSection releaseNotesDetailSectionOneSubSection =
        new ReleaseNotesDetailSubSection()
        {
          Type =
            changelogSubSectionType,
          MarkdownTitle =
            changelogSubSectionType.ToString().ToUpper(),
          MarkdownText =
              "...",
        };


      releaseNotesDetailObservableCollection[0].
        SectionCollection[0].
          SubSectionCollection.
            Add(releaseNotesDetailSectionOneSubSection);
    }

    public void OnPropertyChanged(
      string propertyName
    )
    {
    }

  }

}
