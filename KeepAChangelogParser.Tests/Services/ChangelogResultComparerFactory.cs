using KeepAChangelogParser.Models;
using KeepAChangelogParser.Tests.Comparer;

namespace KeepAChangelogParser.Tests.Services
{

  public static class ChangelogResultComparerFactory
  {

    public static ResultComparer<Changelog> Create()
    {
      ChangelogSubSectionCollectionComparer changelogSubSectionCollectionComparer =
        new ChangelogSubSectionCollectionComparer(
          new ChangelogSubSectionComparer(
            new ChangelogSubSectionItemCollectionComparer(
              new ChangelogSubSectionItemComparer())));

      return new ResultComparer<Changelog>(
          new ChangelogComparer(
            new ChangelogSectionCollectionComparer(
              new ChangelogSectionComparer(
               changelogSubSectionCollectionComparer)),
            new ChangelogSectionUnreleasedComparer(
              changelogSubSectionCollectionComparer)));
    }

  }

}
