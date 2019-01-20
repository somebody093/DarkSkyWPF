using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DarkSkyWPF.Services.DarkSky
{
  /// <summary>
  /// Fetches raw data async from the Dark Sky API.
  /// </summary>
  public class WeatherDataRetriever : IWeatherDataRetriever
  {
    public async Task<string> FetchWeatherData(Uri url)
    {
      using (var client = new HttpClient())
      {
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
      }
    }
  }
}
