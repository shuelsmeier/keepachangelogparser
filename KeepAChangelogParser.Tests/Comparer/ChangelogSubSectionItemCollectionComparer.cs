using KeepAChangelogParser.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace KeepAChangelogParser.Tests.Comparer
{

  public class ChangelogSubSectionItemCollectionComparer :
    IComparer,
    IComparer<ChangelogSubSectionItemCollection>
  {

    private readonly IComparer<ChangelogSubSectionItem> changelogSubSectionItemComparer;

    public ChangelogSubSectionItemCollectionComparer(
      IComparer<ChangelogSubSectionItem> changelogSubSectionItemComparer
    )
    {
      this.changelogSubSectionItemComparer = changelogSubSectionItemComparer;
    }

    public int Compare(
      object? x,
      object? y
    )
    {
      if (!(x is ChangelogSubSectionItemCollection))
      {
        string message =
          string.Format(
            CultureInfo.CurrentCulture,
            "Required input {0} is not a recognized {1}.",
            nameof(y), typeof(ChangelogSubSectionItemCollection));

        throw new ArgumentException(message, nameof(x));
      }

      if (!(y is ChangelogSubSectionItemCollection))
      {
        string message =
          string.Format(
            CultureInfo.CurrentCulture,
            "Required input {0} is not a recognized {1}.",
            nameof(y), typeof(ChangelogSubSectionItemCollection));

        throw new ArgumentException(message, nameof(y));
      }

      int result =
        this.Compare(
          (ChangelogSubSectionItemCollection)x,
          (ChangelogSubSectionItemCollection)y);

      return result;
    }

    public int Compare(
      ChangelogSubSectionItemCollection? x,
      ChangelogSubSectionItemCollection? y
    )
    {
      if (x == null && y != null) { return 1; }
      if (x != null && y == null) { return -1; }

      if (x != null && y != null)
      {
        if (x.Count < y.Count) { return 1; }
        if (x.Count > y.Count) { return -1; }

        for (int i = 0; i < x.Count; i++)
        {
          int result =
            this.compareChangelogSubSectionItemCollection(
              x[i],
              y[i]);

          if (result != 0) { return result; }
        }
      }

      return 0;
    }

    private int compareChangelogSubSectionItemCollection(
      ChangelogSubSectionItem x,
      ChangelogSubSectionItem y
    )
    {
      return this.changelogSubSectionItemComparer.Compare(x, y);
    }

  }

}
