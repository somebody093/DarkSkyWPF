using Newtonsoft.Json;
using System.Collections.Generic;

namespace DarkSkyWPF.Services.DarkSky.JSONModels
{
  /// <summary>
  /// JSON Model helper parent class to be able to parse custom information regarding minutely, hourly or daily weather conditions for the requested location.
  /// <para>Minutely, hourly and daily sections each have a data section containing multiple pieces of individual information (e.g. for each hour or day).</para>
  /// </summary>
  public class MultipleDataPointsWeatherCondition : WeatherCondition
  {
    [JsonProperty("data")]
    public IEnumerable<WeatherDetails> WeatherDetails { get; private set; }
  }
}
