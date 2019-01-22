using Newtonsoft.Json;

namespace DarkSkyWPF.Services.DarkSky.JSONModels
{
  /// <summary>
  /// The DarkSky API's Forecast Request's response parser root object.
  /// </summary>
  public class WeatherDataRoot
  {
    /// <summary>
    /// City name appended after parsing based-on the city model object that was used for the request.
    /// </summary>
    [JsonIgnore]
    public string CityName { get; set; }

    /// <summary>
    /// Stores custom information regarding the current weather conditions at the requested location.
    /// </summary>
    [JsonProperty("currently")]
    public SingleDataPointWeatherCondition CurrentWeather { get; private set; }

    /// <summary>
    /// Stores custom information regarding the minute-by-minute weather conditions for the next hour at the requested location.
    /// </summary>
    [JsonProperty("minutely")]
    public MultipleDataPointsWeatherCondition MinutelyWeather { get; private set; }

    /// <summary>
    /// Stores custom information regarding the hour-by-hour weather conditions for the next two days at the requested location.
    /// </summary>
    [JsonProperty("hourly")]
    public MultipleDataPointsWeatherCondition HourlyWeather { get; private set; }

    /// <summary>
    /// Stores custom information regarding the day-by-day weather conditions for the next week at the requested location.
    /// </summary>
    [JsonProperty("daily")]
    public MultipleDataPointsWeatherCondition DailyWeather { get; private set; }

    /// <summary>
    /// Stores severe weather alert information for the requested location.
    /// </summary>
    [JsonProperty("alerts")]
    public WeatherAlert WeatherAlert { get; private set; }

    /// <summary>
    /// Stores miscellaneous metadata about the DarkSky API request.
    /// </summary>
    [JsonProperty("flags")]
    public RequestMetaData WeatherRequestMetaData { get; private set; }
  }  
}
