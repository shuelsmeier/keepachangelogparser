using KeepAChangelogParser.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace KeepAChangelogParser.Tests.Comparer
{

  public class ChangelogSubSectionCollectionComparer :
    IComparer,
    IComparer<ChangelogSubSectionCollection>
  {

    private readonly IComparer<ChangelogSubSection> changelogSubSectionComparer;

    public ChangelogSubSectionCollectionComparer(
      IComparer<ChangelogSubSection> changelogSubSectionComparer
    )
    {
      this.changelogSubSectionComparer = changelogSubSectionComparer;
    }

    public int Compare(
      object? x,
      object? y
    )
    {
      if (!(x is ChangelogSubSectionCollection))
      {
        string message =
          string.Format(
            CultureInfo.CurrentCulture,
            "Required input {0} is not a recognized {1}.",
            nameof(y), typeof(ChangelogSubSectionCollection));

        throw new ArgumentException(message, nameof(x));
      }

      if (!(y is ChangelogSubSectionCollection))
      {
        string message =
          string.Format(
            CultureInfo.CurrentCulture,
            "Required input {0} is not a recognized {1}.",
            nameof(y), typeof(ChangelogSubSectionCollection));

        throw new ArgumentException(message, nameof(y));
      }

      int result =
        this.Compare(
          (ChangelogSubSectionCollection)x,
          (ChangelogSubSectionCollection)y);

      return result;
    }

    public int Compare(
      ChangelogSubSectionCollection? x,
      ChangelogSubSectionCollection? y
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
            this.compareChangelogSubSectionCollection(
              x[i],
              y[i]);

          if (result != 0) { return result; }
        }
      }

      return 0;
    }

    private int compareChangelogSubSectionCollection(
      ChangelogSubSection x,
      ChangelogSubSection y
    )
    {
      return this.changelogSubSectionComparer.Compare(x, y);
    }

  }

}
