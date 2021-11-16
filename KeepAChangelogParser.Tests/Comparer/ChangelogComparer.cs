using KeepAChangelogParser.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace KeepAChangelogParser.Tests.Comparer
{

  public class ChangelogComparer :
    IComparer<Changelog>
  {

    private readonly IComparer<ChangelogSectionCollection> changelogSectionCollectionComparer;
    private readonly IComparer<ChangelogSectionUnreleased> changelogSectionUnreleasedComparer;

    public ChangelogComparer(
      IComparer<ChangelogSectionCollection> changelogSectionCollectionComparer,
      IComparer<ChangelogSectionUnreleased> changelogSectionUnreleasedComparer
    )
    {
      this.changelogSectionCollectionComparer = changelogSectionCollectionComparer;
      this.changelogSectionUnreleasedComparer = changelogSectionUnreleasedComparer;
    }

    public int Compare(
      object? x,
      object? y
    )
    {
      if (x is not Changelog)
      {
        string message =
          string.Format(
            CultureInfo.CurrentCulture,
            "Required input {0} is not a recognized {1}.",
            nameof(y), typeof(Changelog));

        throw new ArgumentException(message, nameof(x));
      }

      if (y is not Changelog)
      {
        string message =
          string.Format(
            CultureInfo.CurrentCulture,
            "Required input {0} is not a recognized {1}.",
            nameof(y), typeof(Changelog));

        throw new ArgumentException(message, nameof(y));
      }

      int result =
        this.Compare(
          (Changelog)x,
          (Changelog)y);

      return result;
    }

    public int Compare(
      Changelog? x,
      Changelog? y
    )
    {
      if (x == null && y != null) { return 1; }
      if (x != null && y == null) { return -1; }

      if (x != null && y != null)
      {
        return this.compareChangelog(x, y);
      }

      return 0;
    }

    private int compareChangelog(
      Changelog x,
      Changelog y
    )
    {
      int result;

      if ((result = compare(x.MarkdownText, y.MarkdownText)) != 0) { return result; }
      if ((result = compare(x.MarkdownTitle, y.MarkdownTitle)) != 0) { return result; }
      if ((result = this.compare(x.SectionCollection, y.SectionCollection)) != 0) { return result; }
      if ((result = this.compare(x.SectionUnreleased, y.SectionUnreleased)) != 0) { return result; }

      return result;
    }

    private static int compare(
      string x,
      string y
    )
    {
      return string.Compare(x, y, StringComparison.Ordinal);
    }

    private int compare(
      ChangelogSectionCollection? x,
      ChangelogSectionCollection? y
    )
    {
      if (x == null && y != null) { return 1; }
      if (x != null && y == null) { return -1; }

      if (x != null && y != null)
      {
        return this.changelogSectionCollectionComparer.Compare(x, y);
      }

      return 0;
    }

    private int compare(
      ChangelogSectionUnreleased? x,
      ChangelogSectionUnreleased? y
    )
    {
      if (x == null && y != null) { return 1; }
      if (x != null && y == null) { return -1; }

      if (x != null && y != null)
      {
        return this.changelogSectionUnreleasedComparer.Compare(x, y);
      }

      return 0;
    }

  }

}
