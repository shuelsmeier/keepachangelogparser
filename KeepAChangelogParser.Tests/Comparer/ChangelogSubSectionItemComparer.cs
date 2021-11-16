using KeepAChangelogParser.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace KeepAChangelogParser.Tests.Comparer
{

  public class ChangelogSubSectionItemComparer :
    IComparer,
    IComparer<ChangelogSubSectionItem>
  {

    public int Compare(
      object? x,
      object? y
    )
    {
      if (x is not ChangelogSubSectionItem)
      {
        string message =
          string.Format(
            CultureInfo.CurrentCulture,
            "Required input {0} is not a recognized {1}.",
            nameof(y), typeof(ChangelogSubSectionItem));

        throw new ArgumentException(message, nameof(x));
      }

      if (y is not ChangelogSubSectionItem)
      {
        string message =
          string.Format(
            CultureInfo.CurrentCulture,
            "Required input {0} is not a recognized {1}.",
            nameof(y), typeof(ChangelogSubSectionItem));

        throw new ArgumentException(message, nameof(y));
      }

      int result =
        this.Compare(
          (ChangelogSubSectionItem)x,
          (ChangelogSubSectionItem)y);

      return result;
    }

    public int Compare(
      ChangelogSubSectionItem? x,
      ChangelogSubSectionItem? y
    )
    {
      if (x == null && y != null) { return 1; }
      if (x != null && y == null) { return -1; }

      if (x != null && y != null)
      {
        return compareChangelogSubSectionItem(x, y);
      }

      return 0;
    }

    private static int compareChangelogSubSectionItem(
      ChangelogSubSectionItem x,
      ChangelogSubSectionItem y
    )
    {
      int result;

      if ((result = compare(x.MarkdownText, y.MarkdownText)) != 0) { return result; }

      return result;
    }

    private static int compare(
      string x,
      string y
    )
    {
      return string.Compare(x, y, StringComparison.Ordinal);
    }

  }

}
