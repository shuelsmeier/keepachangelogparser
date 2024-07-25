using KeepAChangelogParser.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace KeepAChangelogParser.Tests.Comparer
{
  public class ChangelogSectionUnreleasedComparer :
    IComparer,
    IComparer<ChangelogSectionUnreleased>

  {
    private readonly IComparer<ChangelogSubSectionCollection> changelogSubSectionCollectionComparer;

    public ChangelogSectionUnreleasedComparer(
      IComparer<ChangelogSubSectionCollection> changelogSubSectionCollectionComparer
    )
    {
      this.changelogSubSectionCollectionComparer = changelogSubSectionCollectionComparer;
    }

    public int Compare(
      object? x,
      object? y
    )
    {
      if (x is not ChangelogSectionUnreleased)
      {
        string message =
          string.Format(
            CultureInfo.CurrentCulture,
            "Required input {0} is not a recognized {1}.",
            nameof(y), typeof(ChangelogSectionUnreleased));

        throw new ArgumentException(message, nameof(x));
      }

      if (y is not ChangelogSectionUnreleased)
      {
        string message =
          string.Format(
            CultureInfo.CurrentCulture,
            "Required input {0} is not a recognized {1}.",
            nameof(y), typeof(ChangelogSectionUnreleased));

        throw new ArgumentException(message, nameof(y));
      }

      int result =
        this.Compare(
          (ChangelogSectionUnreleased)x,
          (ChangelogSectionUnreleased)y);

      return result;
    }

    public int Compare(
      ChangelogSectionUnreleased? x,
      ChangelogSectionUnreleased? y
    )
    {
      if (x == null && y != null) { return 1; }
      if (x != null && y == null) { return -1; }

      if (x != null && y != null)
      {
        return this.compareChangelogSectionUnreleased(x, y);
      }

      return 0;
    }

    private int compareChangelogSectionUnreleased(
      ChangelogSectionUnreleased x,
      ChangelogSectionUnreleased y
    )
    {
      int result;

      if ((result = compare(x.MarkdownTitle, y.MarkdownTitle)) != 0) { return result; }
      if ((result = this.compare(x.SubSectionCollection, y.SubSectionCollection)) != 0) { return result; }

      return result;
    }

    private static int compare(
      string x,
      string y
    )
    {
      return string.CompareOrdinal(x, y);
    }

    private int compare(
      ChangelogSubSectionCollection? x,
      ChangelogSubSectionCollection? y
    )
    {
      if (x == null && y != null) { return 1; }
      if (x != null && y == null) { return -1; }

      if (x != null && y != null)
      {
        return this.changelogSubSectionCollectionComparer.Compare(x, y);
      }

      return 0;
    }
  }
}
