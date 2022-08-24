using KeepAChangelogParser.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace KeepAChangelogParser.Tests.Comparer
{
  public class ChangelogSectionComparer :
    IComparer<ChangelogSection>
  {
    private readonly IComparer<ChangelogSubSectionCollection> changelogSubSectionCollectionComparer;

    public ChangelogSectionComparer(
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
      if (x is not ChangelogSection)
      {
        string message =
          string.Format(
            CultureInfo.CurrentCulture,
            "Required input {0} is not a recognized {1}.",
            nameof(y), typeof(ChangelogSection));

        throw new ArgumentException(message, nameof(x));
      }

      if (y is not ChangelogSection)
      {
        string message =
          string.Format(
            CultureInfo.CurrentCulture,
            "Required input {0} is not a recognized {1}.",
            nameof(y), typeof(ChangelogSection));

        throw new ArgumentException(message, nameof(y));
      }

      int result =
        this.Compare(
          (ChangelogSection)x,
          (ChangelogSection)y);

      return result;
    }

    public int Compare(
      ChangelogSection? x,
      ChangelogSection? y
    )
    {
      if (x == null && y != null) { return 1; }
      if (x != null && y == null) { return -1; }

      if (x != null && y != null)
      {
        return this.compareChangelogSection(x, y);
      }

      return 0;
    }

    private int compareChangelogSection(
      ChangelogSection x,
      ChangelogSection y
    )
    {
      int result;

      if ((result = compare(x.MarkdownDate, y.MarkdownDate)) != 0) { return result; }
      if ((result = compare(x.MarkdownVersion, y.MarkdownVersion)) != 0) { return result; }
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
