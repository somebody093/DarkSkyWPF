using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DarkSkyWPF.Converters
{
  /// <summary>
  /// Converts UNIX time to regular DateTime and back.
  /// </summary>
  public class UNIXTimeToDateConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      double timeStamp;
      if (double.TryParse(value.ToString(), out timeStamp))
      {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return dateTime.AddSeconds(timeStamp);
      }
      return Properties.Resources.UNIXTimeToDateConvertErrorMessage;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      DateTime dateTime;
      if (DateTime.TryParse(value.ToString(), out dateTime))
      {
        return dateTime.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
      }
      return DependencyProperty.UnsetValue;
    }
  }
}
