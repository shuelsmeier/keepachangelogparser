using CSharpFunctionalExtensions;
using KeepAChangelogParser.Models;
using KeepAChangelogParser.Wpf.SampleApp.Contracts;
using KeepAChangelogParser.Wpf.SampleApp.Contracts.ReleaseNotesWindow;
using KeepAChangelogParser.Wpf.SampleApp.Models.ReleaseNotesWindow;
using System;

namespace KeepAChangelogParser.Wpf.SampleApp.Services.ReleaseNotesWindow
{

  public class ReleaseNotesWindowService :
    IReleaseNotesWindowService
  {

    private readonly IReleaseNotesWindowViewModel releaseNotesWindowViewModel;
    private readonly IChangelogService changelogService;

    private bool isInitialized = false;

    public ReleaseNotesWindowService(
      IReleaseNotesWindowViewModel releaseNotesWindowViewModel,
      IChangelogService changelogService
    )
    {
      this.releaseNotesWindowViewModel = releaseNotesWindowViewModel;
      this.changelogService = changelogService;
    }


    public Result Initialize()
    {
      if (!this.isInitialized)
      {
        Result<Changelog> readChangelogResult =
          this.changelogService.ReadChangelog(
            Environment.CurrentDirectory + "\\SampleChangelog.md");

        if (readChangelogResult.IsFailure) { return readChangelogResult; }

        Changelog changelog =
          readChangelogResult.Value;

        ReleaseNotesDetail releaseNotesDetail =
          new ReleaseNotesDetail()
          {
            MarkdownTitle = $"# {changelog.MarkdownTitle}",
            MarkdownText = changelog.MarkdownText,
          };

        if (changelog.SectionUnreleased != null)
        {
          ReleaseNotesDetailSection releaseNotesDetailSection =
            new ReleaseNotesDetailSection()
            {
              MarkdownTitle = changelog.SectionUnreleased.MarkdownTitle
            };

          foreach (ChangelogSubSection changelogSubSection in changelog.SectionUnreleased.SubSectionCollection)
          {
            foreach (ChangelogSubSectionItem item in changelogSubSection.ItemCollection)
            {
              ReleaseNotesDetailSubSection releaseNotesDetailSubSection =
                new ReleaseNotesDetailSubSection()
                {
                  Type = changelogSubSection.Type,
                  MarkdownTitle = changelogSubSection.Type.ToString().ToUpper(),
                  MarkdownText = item.MarkdownText,
                };

              releaseNotesDetailSection.
                SubSectionCollection.Add(
                  releaseNotesDetailSubSection);
            }
          }

          releaseNotesDetail.
            SectionCollection.Add(
              releaseNotesDetailSection);
        }

        foreach (ChangelogSection changelogSection in changelog.SectionCollection)
        {
          ReleaseNotesDetailSection releaseNotesDetailSection =
            new ReleaseNotesDetailSection()
            {
              MarkdownTitle = changelogSection.MarkdownVersion,
              MarkdownDate = $"released on {changelogSection.MarkdownDate}",
            };

          foreach (ChangelogSubSection changelogSubSection in changelogSection.SubSectionCollection)
          {
            foreach (ChangelogSubSectionItem item in changelogSubSection.ItemCollection)
            {
              ReleaseNotesDetailSubSection releaseNotesDetailSubSection =
                new ReleaseNotesDetailSubSection()
                {
                  Type = changelogSubSection.Type,
                  MarkdownTitle = changelogSubSection.Type.ToString().ToUpper(),
                  MarkdownText = item.MarkdownText,
                };

              releaseNotesDetailSection.
                SubSectionCollection.Add(
                  releaseNotesDetailSubSection);
            }
          }

          releaseNotesDetail.
            SectionCollection.Add(
              releaseNotesDetailSection);
        }

        this.releaseNotesWindowViewModel.DataGridItemsSourceCollection.Add(
          releaseNotesDetail);

        this.releaseNotesWindowViewModel.OnPropertyChanged(
          nameof(this.releaseNotesWindowViewModel.DataGridItemsSourceCollection));
      }

      this.isInitialized = true;

      return Result.Success();
    }

  }

}
