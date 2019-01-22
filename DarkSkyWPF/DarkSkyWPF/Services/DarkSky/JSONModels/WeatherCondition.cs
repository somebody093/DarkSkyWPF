using Newtonsoft.Json;

namespace DarkSkyWPF.Services.DarkSky.JSONModels
{
  /// <summary>
  /// JSON Model helper parent class. All weather-specific response sections (currently, minutely, hourly, daily) contain a general summary information and a summary weather icon for the concerned period.
  /// </summary>
  public class WeatherCondition
  {
    private const string ImageRelativePath = @"\Images\";
    private const string FileNameEnding = ".png";

    private string _imageSource;

    [JsonProperty("summary")]
    public string Summary { get; private set; }

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
          catch (System.ArgumentNullException)
          {
            //logging
          }

          return _imageSource;
        }
        return _imageSource;
      }
    }
  }
}
