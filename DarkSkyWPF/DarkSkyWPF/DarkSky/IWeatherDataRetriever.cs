using System;
using System.Threading.Tasks;

namespace DarkSkyWPF.DarkSky
{
  /// <summary>
  /// Fetches raw data async from the Dark Sky API.
  /// </summary>
  public interface IWeatherDataRetriever
  {
    Task<string> FetchWeatherData(Uri url);
  }
}
