namespace DarkSkyWPF.Services.DarkSky
{
  /// <summary>
  /// The DarkSky API Forecast Request can be added an optional exclude query parameter to specifically not receive all sections from the API.
  /// Only values not essential for the UI can be left out (e.g. the currently block is required by this application)
  /// </summary>
  public enum ExcludeParameter
  {
    None,
    Minutely,
    Hourly,
    Alerts,
    Flags,
    AlertsAndFlags,
    AllExceptDailyAndCurrently
  }
}
