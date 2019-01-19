using Newtonsoft.Json;

namespace DarkSkyWPF.DarkSky.JSONModels
{
  /// <summary>
  /// JSON Model helper class to be able to store the different Forecast Request's data points' detailed data. Each MultipleDataPointsWeatherCondition (minutely, hourly and daily) section contains multiple pieces of WeatherDetails inside their [data] section.
  /// <para>The descriptions are sometimes outdated even on DarkSky.net but they are quoted from there.</para>
  /// </summary>
  public class WeatherDetails
  {
    /// <summary>
    /// The apparent (or “feels like”) temperature in degrees Fahrenheit. Optional, only on hourly.
    /// </summary>
    [JsonProperty("apparentTemperature")]
    public double ApparentTemperature { get; set; }

    /// <summary>
    /// The daytime high apparent temperature. Optional, only on daily.
    /// </summary>
    [JsonProperty("apparentTemperatureHigh")]
    public double ApparentTemperatureHighest { get; set; }

    /// <summary>
    /// The overnight low apparent temperature. Optional, only on daily.
    /// </summary>
    [JsonProperty("apparentTemperatureLow")]
    public double ApparentTemperatureLowest { get; set; }

    /// <summary>
    /// The sea-level air pressure in millibars.
    /// </summary>
    [JsonProperty("pressure")]
    public double AtmosphericPressure { get; set; }

    /// <summary>
    /// The machine-readable icon suggestion for a data section entity (an hour, minute or day).
    /// </summary>
    [JsonProperty("icon")]
    public string Icon { get; set; }

    /// <summary>
    /// The relative humidity, between 0 and 1, inclusive.
    /// </summary>
    [JsonProperty("humidity")]
    public double Humidity { get; set; }

    /// <summary>
    /// The human-readable summary for a data section entity (an hour, minute or day).
    /// </summary>
    [JsonProperty("summary")]
    public string Summary { get; set; }

    /// <summary>
    /// The air temperature in degrees Fahrenheit. Optional, only on hourly.
    /// </summary>
    [JsonProperty("temperature")]
    public double Temperature { get; set; }

    /// <summary>
    /// The daytime high temperature. Only on daily.
    /// </summary>
    [JsonProperty("temperatureHigh")]
    public double TemperatureHighest { get; set; }

    /// <summary>
    /// The overnight low temperature. Only on daily.
    /// </summary>
    [JsonProperty("temperatureLow")]
    public double TemperatureLowest { get; set; }

    /// <summary>
    /// The UNIX time at which this data point begins.
    /// </summary>
    [JsonProperty("time")]
    public long UNIXTime { get; set; }

    /// <summary>
    /// The UV index.
    /// </summary>
    [JsonProperty("uvIndex")]
    public double UVIndex { get; set; }

    /// <summary>
    /// The wind speed in miles per hour.
    /// </summary>
    [JsonProperty("windSpeed")]
    public double WindSpeed { get; set; }
  }
}
