namespace DarkSkyWPF.DarkSky
{
  /// <summary>
  /// The DarkSky API Forecast Request can be added an optional exclude query parameter to specifically not receive all sections from the API.
  /// </summary>
  public enum ExcludeParameter
  {
    None,
    Currently,
    Minutely,
    Hourly,
    Daily,
    Alerts,
    Flags,
    CurrentlyAndMinutely,
    CurrentlyAndHourly,
    AlertsAndFlags,
    AllExceptDaily,
    AllExceptDailyAndCurrently
  }
}
