using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DarkSkyWPF.DarkSky
{
    /// <summary>
    /// Fetches raw data async from the Dark Sky API
    /// </summary>
    public class WeatherDataRetriever : IWeatherDataRetriever
    {
        public async Task<string> FetchWeatherData(string url)
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
