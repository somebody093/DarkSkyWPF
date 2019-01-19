using Newtonsoft.Json;

namespace DarkSkyWPF.DarkSky.JSONModels
{
  /// <summary>
  /// JSON Model helper class to be able to parse severe weather alert information for the requested location.
  /// </summary>
  public class WeatherAlert
  {
    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("severity")]
    public string UnitsType { get; set; }
  }
}
