using KeepAChangelogParser.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace KeepAChangelogParser.Tests.Comparer
{
  public class ChangelogSubSectionComparer :
    IComparer<ChangelogSubSection>
  {
    private readonly IComparer<ChangelogSubSectionItemCollection> changelogSubSectionItemCollectionComparer;

    public ChangelogSubSectionComparer(
      IComparer<ChangelogSubSectionItemCollection> changelogSubSectionItemCollectionComparer
    )
    {
      this.changelogSubSectionItemCollectionComparer = changelogSubSectionItemCollectionComparer;
    }

    public int Compare(
      object? x,
      object? y
    )
    {
      if (x is not ChangelogSubSection)
      {
        string message =
          string.Format(
            CultureInfo.CurrentCulture,
            "Required input {0} is not a recognized {1}.",
            nameof(y), typeof(ChangelogSubSection));

        throw new ArgumentException(message, nameof(x));
      }

      if (y is not ChangelogSubSection)
      {
        string message =
          string.Format(
            CultureInfo.CurrentCulture,
            "Required input {0} is not a recognized {1}.",
            nameof(y), typeof(ChangelogSubSection));

        throw new ArgumentException(message, nameof(y));
      }

      int result =
        this.Compare(
          (ChangelogSubSection)x,
          (ChangelogSubSection)y);

      return result;
    }

    public int Compare(
      ChangelogSubSection? x,
      ChangelogSubSection? y
    )
    {
      if (x == null && y != null) { return 1; }
      if (x != null && y == null) { return -1; }

      if (x != null && y != null)
      {
        return this.compareChangelogSubSection(x, y);
      }

      return 0;
    }

    private int compareChangelogSubSection(
      ChangelogSubSection x,
      ChangelogSubSection y
    )
    {
      int result;

      if ((result = compare(x.Type, y.Type)) != 0) { return result; }
      if ((result = compare(x.CustomType, y.CustomType)) != 0) { return result; }
      if ((result = this.compare(x.ItemCollection, y.ItemCollection)) != 0) { return result; }

      return result;
    }

    private static int compare(
      ChangelogSubSectionType x,
      ChangelogSubSectionType y
    )
    {
      return string.CompareOrdinal(Enum.GetName(x), Enum.GetName(y));
    }

    private static int compare(
      string? x,
      string? y
    )
    {
      if (x == null && y != null) { return 1; }
      if (x != null && y == null) { return -1; }

      if (x != null && y != null)
      {
        return string.CompareOrdinal(x, y);
      }

      return 0;
    }

    private int compare(
      ChangelogSubSectionItemCollection x,
      ChangelogSubSectionItemCollection y
    )
    {
      return this.changelogSubSectionItemCollectionComparer.Compare(x, y);
    }
  }
}
