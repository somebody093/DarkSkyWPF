using log4net;
using Newtonsoft.Json;
using System;

namespace DarkSkyWPF.Services.DarkSky.JSONModels
{
  /// <summary>
  /// JSON Model helper class to be able to store the different Forecast Request's data points' detailed data. Each MultipleDataPointsWeatherCondition (minutely, hourly and daily) section contains multiple pieces of WeatherDetails inside their [data] section.
  /// <para>The descriptions are sometimes outdated even on DarkSky.net but they are quoted from there.</para>
  /// <para>This class also calculates the missing averages from the daily fields</para>
  /// </summary>
  public class WeatherDetails
  {
    private static readonly ILog Logger = LogManager.GetLogger(typeof(WeatherDetails));

    private const string ImageRelativePath = @"\Images\";
    private const string FileNameEnding = ".png";

    private double? _apparentTemperatureCalculated;
    private double? _temperatureCalculated;
    private string _imageSource;

    /// <summary>
    /// The apparent (or “feels like”) temperature in degrees Fahrenheit. Optional, only on hourly.
    /// </summary>
    [JsonProperty("apparentTemperature")]
    public double? ApparentTemperature { get; private set; }

    /// <summary>
    /// The daytime high apparent temperature. Optional, only on daily.
    /// </summary>
    [JsonProperty("apparentTemperatureHigh")]
    public double? ApparentTemperatureHighest { get; private set; }

    /// <summary>
    /// The overnight low apparent temperature. Optional, only on daily.
    /// </summary>
    [JsonProperty("apparentTemperatureLow")]
    public double? ApparentTemperatureLowest { get; private set; }

    /// <summary>
    /// Simple average ApparentTemperature due to the missing original property on daily objects
    /// </summary>
    [JsonIgnore]
    public double? ApparentTemperatureCalculated
    {
      get
      {
        //no calculation needed, in case available
        if (ApparentTemperature != null)
        {
          return ApparentTemperature;
        }

        if (_apparentTemperatureCalculated != null)
        {
          return _apparentTemperatureCalculated;
        }

        if (ApparentTemperatureHighest == null || ApparentTemperatureLowest == null)
        {
          _apparentTemperatureCalculated = null;
          Logger.Warn("ApparentTemperatureHighest or ApparentTemperatureLowest was not available this time.");
        }
        else
        {
          _apparentTemperatureCalculated = (ApparentTemperatureHighest + ApparentTemperatureLowest) / 2;
        }

        return _apparentTemperatureCalculated;
      }
    }

    /// <summary>
    /// The sea-level air pressure in millibars.
    /// </summary>
    [JsonProperty("pressure")]
    public double? AtmosphericPressure { get; private set; }

    /// <summary>
    /// The machine-readable icon suggestion for a data section entity (an hour, minute or day).
    /// </summary>
    [JsonProperty("icon")]
    public IconValue Icon { get; private set; }

    /// <summary>
    /// Used to be able to locate the replacement of the Icon string; an actual image resource for the ui
    /// </summary>
    [JsonIgnore]
    public string ImageSource
    {
      get
      {
        if (_imageSource == null)
        {
          try
          {
            _imageSource = ImageRelativePath + System.Enum.GetName(typeof(IconValue), Icon) + FileNameEnding;
          }
          catch (ArgumentNullException ex)
          {
            Logger.Error("Image is not available. Icon parsing to IconValue has failed.", ex);
            _imageSource = null;
          }

          return _imageSource;
        }
        return _imageSource;
      }
    }

    /// <summary>
    /// The relative humidity, between 0 and 1, inclusive.
    /// </summary>
    [JsonProperty("humidity")]
    public double? Humidity { get; private set; }

    /// <summary>
    /// The human-readable summary for a data section entity (an hour, minute or day).
    /// </summary>
    [JsonProperty("summary")]
    public string Summary { get; private set; }

    /// <summary>
    /// The air temperature in degrees Fahrenheit. Optional, only on hourly.
    /// </summary>
    [JsonProperty("temperature")]
    public double? Temperature { get; private set; }

    /// <summary>
    /// The daytime high temperature. Only on daily.
    /// </summary>
    [JsonProperty("temperatureHigh")]
    public double? TemperatureHighest { get; private set; }

    /// <summary>
    /// The overnight low temperature. Only on daily.
    /// </summary>
    [JsonProperty("temperatureLow")]
    public double? TemperatureLowest { get; private set; }

    /// <summary>
    /// Simple average Temperature calculated due to the missing original property on daily objects
    /// </summary>
    [JsonIgnore]
    public double? TemperatureCalculated
    {
      get
      {
        //no calculation needed, in case available
        if (Temperature != null)
        {
          return Temperature;
        }

        if (_temperatureCalculated != null)
        {
          return _temperatureCalculated;
        }

        if (TemperatureHighest == null || TemperatureLowest == null)
        {
          Logger.Warn("TemperatureHighest or TemperatureLowest was not available this time.");
          _temperatureCalculated = null;
        }
        else
        {
          _temperatureCalculated = (TemperatureHighest + TemperatureLowest) / 2;
        }
        return _temperatureCalculated;
      }
    }

    /// <summary>
    /// The UNIX time at which this data point begins.
    /// </summary>
    [JsonProperty("time")]
    public double? UNIXTime { get; private set; }

    /// <summary>
    /// The UV index.
    /// </summary>
    [JsonProperty("uvIndex")]
    public double? UVIndex { get; private set; }

    /// <summary>
    /// The wind speed in miles per hour.
    /// </summary>
    [JsonProperty("windSpeed")]
    public double? WindSpeed { get; private set; }
  }
}
