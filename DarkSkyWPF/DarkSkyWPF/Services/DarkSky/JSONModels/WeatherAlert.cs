using Newtonsoft.Json;

namespace DarkSkyWPF.Services.DarkSky.JSONModels
{
  /// <summary>
  /// JSON Model helper class to be able to parse severe weather alert information for the requested location.
  /// </summary>
  public class WeatherAlert
  {
    [JsonProperty("description")]
    public string Description { get; private set; }

    [JsonProperty("title")]
    public string Title { get; private set; }

    [JsonProperty("severity")]
    public string UnitsType { get; private set; }
  }
}
