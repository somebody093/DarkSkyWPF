using Newtonsoft.Json;
using System.Collections.Generic;

namespace DarkSkyWPF.DarkSky.JSONModels
{
  /// <summary>
  /// The DarkSky API's Forecast Request's response parser root object.
  /// </summary>
  public class WeatherDataRoot
  {
    /// <summary>
    /// Stores custom information regarding the current weather conditions at the requested location.
    /// </summary>
    [JsonProperty("currently")]
    public SingleDataPointWeatherCondition CurrentWeather { get; set; }

    /// <summary>
    /// Stores custom information regarding the minute-by-minute weather conditions for the next hour at the requested location.
    /// </summary>
    [JsonProperty("minutely")]
    public MultipleDataPointsWeatherCondition MinutelyWeather { get; set; }

    /// <summary>
    /// Stores custom information regarding the hour-by-hour weather conditions for the next two days at the requested location.
    /// </summary>
    [JsonProperty("hourly")]
    public MultipleDataPointsWeatherCondition HourlyWeather { get; set; }

    /// <summary>
    /// Stores custom information regarding the day-by-day weather conditions for the next week at the requested location.
    /// </summary>
    [JsonProperty("daily")]
    public MultipleDataPointsWeatherCondition DailyWeather { get; set; }

    /// <summary>
    /// Stores severe weather alert information for the requested location.
    /// </summary>
    [JsonProperty("alerts")]
    public WeatherAlert WeatherAlert { get; set; }

    /// <summary>
    /// Stores miscellaneous metadata about the DarkSky API request.
    /// </summary>
    [JsonProperty("flags")]
    public RequestMetaData WeatherRequestMetaData { get; set; }
  }

  /// <summary>
  /// JSON Model helper parent class. All weather-specific response sections (currently, minutely, hourly, daily) contain a general summary information and a summary weather icon for the concerned period.
  /// </summary>
  public class WeatherCondition
  {
    [JsonProperty("summary")]
    public string Summary { get; set; }

    [JsonProperty("icon")]
    public string Icon { get; set; }
  }

  /// <summary>
  /// JSON Model helper class to be able to parse custom information regarding the current weather conditions at the requested location.
  /// </summary>
  public class SingleDataPointWeatherCondition : WeatherCondition
  {
    [JsonProperty("apparentTemperature")]
    public double ApparentTemperature { get; set; }

    [JsonProperty("temperature")]
    public double Temperature { get; set; }    

    [JsonProperty("humidity")]
    public double Humidity { get; set; }

    [JsonProperty("pressure")]
    public double AtmosphericPressure { get; set; }

    [JsonProperty("time")]
    public long UNIXTime { get; set; }

    [JsonProperty("uvIndex")]
    public double UVIndex { get; set; }

    [JsonProperty("windSpeed")]
    public double WindSpeed { get; set; }
  }

  /// <summary>
  /// JSON Model helper parent class to be able to parse custom information regarding minutely, hourly or daily weather conditions for the requested location.
  /// <para>Minutely, hourly and daily sections each have a data section containing multiple pieces of individual information (e.g. for each hour or day).</para>
  /// </summary>
  public class MultipleDataPointsWeatherCondition : WeatherCondition
  {
    [JsonProperty("data")]
    public IEnumerable<WeatherDetails> WeatherDetails { get; set; }
  }
}
