using Newtonsoft.Json;
using System;

namespace DarkSkyWPF.Services.JSONModels
{
  /// <summary>
  /// JSON Model helper class to be able to store the different Forecast Request's data points' detailed data. Each MultipleDataPointsWeatherCondition (minutely, hourly and daily) section contains multiple pieces of WeatherDetails inside their [data] section.
  /// <para>The descriptions are sometimes outdated even on DarkSky.net but they are quoted from there.</para>
  /// todo: kiegészíteni
  /// </summary>
  public class WeatherDetails
  {
    private double? _apparentTemperatureCalculated;
    private double? _temperatureCalculated;

    /// <summary>
    /// The apparent (or “feels like”) temperature in degrees Fahrenheit. Optional, only on hourly.
    /// </summary>
    [JsonProperty("apparentTemperature")]
    public double ApparentTemperature { get; private set; }

    /// <summary>
    /// The daytime high apparent temperature. Optional, only on daily.
    /// </summary>
    [JsonProperty("apparentTemperatureHigh")]
    public double ApparentTemperatureHighest { get; private set; }

    /// <summary>
    /// The overnight low apparent temperature. Optional, only on daily.
    /// </summary>
    [JsonProperty("apparentTemperatureLow")]
    public double ApparentTemperatureLowest { get; private set; }

    /// <summary>
    /// tba
    /// </summary>
    [JsonIgnore]
    public double? ApparentTemperatureCalculated
    {
      get
      {
        try
        {
          if (_apparentTemperatureCalculated != null)
          {
            return _apparentTemperatureCalculated;
          }

          _apparentTemperatureCalculated = (ApparentTemperatureHighest + ApparentTemperatureLowest) / 2;
          return _apparentTemperatureCalculated;
        }
        catch (Exception)
        {
          //tba
          return null;
        }
      }
    }

    /// <summary>
    /// The sea-level air pressure in millibars.
    /// </summary>
    [JsonProperty("pressure")]
    public double AtmosphericPressure { get; private set; }

    /// <summary>
    /// The machine-readable icon suggestion for a data section entity (an hour, minute or day).
    /// </summary>
    [JsonProperty("icon")]
    public string Icon { get; private set; }

    /// <summary>
    /// The relative humidity, between 0 and 1, inclusive.
    /// </summary>
    [JsonProperty("humidity")]
    public double Humidity { get; private set; }

    /// <summary>
    /// The human-readable summary for a data section entity (an hour, minute or day).
    /// </summary>
    [JsonProperty("summary")]
    public string Summary { get; private set; }

    /// <summary>
    /// The air temperature in degrees Fahrenheit. Optional, only on hourly.
    /// </summary>
    [JsonProperty("temperature")]
    public double Temperature { get; private set; }

    /// <summary>
    /// The daytime high temperature. Only on daily.
    /// </summary>
    [JsonProperty("temperatureHigh")]
    public double TemperatureHighest { get; private set; }

    /// <summary>
    /// The overnight low temperature. Only on daily.
    /// </summary>
    [JsonProperty("temperatureLow")]
    public double TemperatureLowest { get; private set; }

    /// <summary>
    /// tba
    /// </summary>
    [JsonIgnore]
    public double? TemperatureCalculated
    {
      get
      {
        try
        {
          if (_temperatureCalculated != null)
          {
            return _temperatureCalculated;
          }

          _temperatureCalculated = (TemperatureHighest + TemperatureLowest) / 2;
          return _temperatureCalculated;
        }
        catch (Exception)
        {
          //tba
          return null;
        }
      }
    }

    /// <summary>
    /// The UNIX time at which this data point begins.
    /// </summary>
    [JsonProperty("time")]
    public long UNIXTime { get; private set; }

    /// <summary>
    /// The UV index.
    /// </summary>
    [JsonProperty("uvIndex")]
    public double UVIndex { get; private set; }

    /// <summary>
    /// The wind speed in miles per hour.
    /// </summary>
    [JsonProperty("windSpeed")]
    public double WindSpeed { get; private set; }
  }
}
