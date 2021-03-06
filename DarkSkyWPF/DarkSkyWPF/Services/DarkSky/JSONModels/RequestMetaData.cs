﻿using Newtonsoft.Json;

namespace DarkSkyWPF.Services.DarkSky.JSONModels
{
  /// <summary>
  /// JSON Model helper class to be able to parse miscellaneous metadata about the DarkSky API request.
  /// </summary>
  public class RequestMetaData
  {
    [JsonProperty("units")]
    public string UnitsType { get; private set; }
  }
}
