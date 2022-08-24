using KeepAChangelogParser.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace KeepAChangelogParser.Tests.Comparer
{
  public class ChangelogSectionCollectionComparer :
    IComparer,
    IComparer<ChangelogSectionCollection>
  {
    private readonly IComparer<ChangelogSection> changelogSectionComparer;

    public ChangelogSectionCollectionComparer(
      IComparer<ChangelogSection> changelogSectionComparer
    )
    {
      this.changelogSectionComparer = changelogSectionComparer;
    }

    public int Compare(
      object? x,
      object? y
    )
    {
      if (x is not ChangelogSectionCollection)
      {
        string message =
          string.Format(
            CultureInfo.CurrentCulture,
            "Required input {0} is not a recognized {1}.",
            nameof(y), typeof(ChangelogSectionCollection));

        throw new ArgumentException(message, nameof(x));
      }

      if (y is not ChangelogSectionCollection)
      {
        string message =
          string.Format(
            CultureInfo.CurrentCulture,
            "Required input {0} is not a recognized {1}.",
            nameof(y), typeof(ChangelogSectionCollection));

        throw new ArgumentException(message, nameof(y));
      }

      int result =
        this.Compare(
          (ChangelogSectionCollection)x,
          (ChangelogSectionCollection)y);

      return result;
    }

    public int Compare(
      ChangelogSectionCollection? x,
      ChangelogSectionCollection? y
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
            this.compareChangelogSectionCollection(
              x[i],
              y[i]);

          if (result != 0) { return result; }
        }
      }

      return 0;
    }

    private int compareChangelogSectionCollection(
      ChangelogSection x,
      ChangelogSection y
    )
    {
      return this.changelogSectionComparer.Compare(x, y);
    }
  }
}
