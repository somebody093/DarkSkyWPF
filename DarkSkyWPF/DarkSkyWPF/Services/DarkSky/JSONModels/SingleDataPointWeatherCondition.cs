using Newtonsoft.Json;

namespace DarkSkyWPF.Services.DarkSky.JSONModels
{
  /// <summary>
  /// JSON Model helper class to be able to parse custom information regarding the current weather conditions at the requested location.
  /// </summary>
  public class SingleDataPointWeatherCondition : WeatherCondition
  {
    [JsonProperty("apparentTemperature")]
    public double? ApparentTemperature { get; private set; }

    [JsonProperty("temperature")]
    public double? Temperature { get; private set; }

    [JsonProperty("humidity")]
    public double? Humidity { get; private set; }

    [JsonProperty("pressure")]
    public double? AtmosphericPressure { get; private set; }

    [JsonProperty("time")]
    public double? UNIXTime { get; private set; }

    [JsonProperty("uvIndex")]
    public double? UVIndex { get; private set; }

    [JsonProperty("windSpeed")]
    public double? WindSpeed { get; private set; }
  }
}
