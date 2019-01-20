using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace DarkSkyWPF.Services.DarkSky.JSONModels
{
  /// <summary>
  /// Enum to be able to map icon values to images
  /// </summary>
  [JsonConverter(typeof(StringEnumConverter))]
  public enum IconValue
  {
    NONE,
    [EnumMember(Value = "clear-day")]
    CLEARDAY,
    [EnumMember(Value = "clear-night")]
    CLEARNIGHT,
    [EnumMember(Value = "rain")]
    RAIN,
    [EnumMember(Value = "snow")]
    SNOW,
    [EnumMember(Value = "sleet")]
    SLEET,
    [EnumMember(Value = "wind")]
    WIND,
    [EnumMember(Value = "fog")]
    FOG,
    [EnumMember(Value = "cloudy")]
    CLOUDY,
    [EnumMember(Value = "partly-cloudy-day")]
    PARTLYCLOUDYDAY,
    [EnumMember(Value = "partly-cloudy-night")]
    PARTLYCLOUDYNIGHT,
    [EnumMember(Value = "hail")]
    HAIL,
    [EnumMember(Value = "thunderstorm")]
    THUNDERSTORM,
    [EnumMember(Value = "tornado")]
    TORNADO
  }
}
