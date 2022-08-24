using Ardalis.GuardClauses;
using KeepAChangelogParser.Models;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace KeepAChangelogParser.Wpf.SampleApp.Services.ReleaseNotesWindow
{
  public class ReleaseNotesSubSectionButtonVisibilityConverter :
    IValueConverter
  {
    public object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture
    )
    {
      _ = Guard.Against.Null(value, nameof(value));

      ChangelogSubSectionType changelogSubSectionType =
        (ChangelogSubSectionType)value;

      switch (changelogSubSectionType)
      {
        case ChangelogSubSectionType.Added: { return "#bcb901"; }
        case ChangelogSubSectionType.Changed: { return "#0486bc"; }
        case ChangelogSubSectionType.Deprecated: { return "#bc7c00"; }
        case ChangelogSubSectionType.Fixed: { return "#00a825"; }
        case ChangelogSubSectionType.Removed: { return "#bc0e00"; }
        case ChangelogSubSectionType.Security: { return "#bc00a8"; }
        default:
          {
            throw new InvalidEnumArgumentException(
              nameof(changelogSubSectionType),
              System.Convert.ToInt32(changelogSubSectionType),
              typeof(ChangelogSubSectionType));
          }
      }
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture
    )
    {
      throw new NotImplementedException();
    }
  }
}
