using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace KeepAChangelogParser.Wpf.SampleApp.Services.ReleaseNotesWindow
{
  public class ReleaseNotesColumnValueConverter :
    IValueConverter
  {
    public object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture
    )
    {
      if (value is not IDictionary<string, object> row)
      {
        return string.Empty;
      }

      string columnName =
        (string)parameter;

      return row[columnName];
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
