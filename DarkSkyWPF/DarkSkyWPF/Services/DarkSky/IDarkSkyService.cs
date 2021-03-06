﻿using DarkSkyWPF.Services.Cities;
using DarkSkyWPF.Services.DarkSky.JSONModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DarkSkyWPF.Services.DarkSky
{
  /// <summary>
  /// IDarkSkyService is responsible for handling service calls and wrapping the raw data coming from the DarkSky API.
  /// </summary>
  public interface IDarkSkyService
  {
    /// <summary>
    /// Requests weather data for a specified city. It is possible to omit some of the response sections.
    /// </summary>
    /// <param name="city">The city the weather request is queried for.</param>
    /// <param name="excludeParameter">By specifying an Exclude Parameter it is possible to not request all weather sections for the given location.</param>
    /// <param name="metricSystem">By specifying a Metric System Parameter it is possible to get the request's metrics in UI, SI or AUTO formats.</param>
    /// <returns>An WeatherDataRoot object with the requested data.</returns>
    Task<WeatherDataRoot> GetWeatherDataForCity(City city, ExcludeParameter excludeParameter = ExcludeParameter.AllExceptDailyAndCurrently, MetricSystem metricSystem = MetricSystem.AUTO);

    /// <summary>
    /// Requests weather data for a list of cities. It is possible to omit some of the response sections.
    /// </summary>
    /// <param name="cities">The list of cities the weather request is queried for.</param>
    /// <param name="excludeParameter">By specifying an Exclude Parameter it is possible to not request all weather sections for the given location.</param>
    /// <param name="metricSystem">By specifying a Metric System Parameter it is possible to get the request's metrics in UI, SI or AUTO formats.</param>
    /// <returns>An IEnumerable<WeatherDataRoot> object with the requested data.</returns>
    Task<IEnumerable<WeatherDataRoot>> GetWeatherDataForMultipleCities(IEnumerable<City> cities, ExcludeParameter excludeParameter = ExcludeParameter.AllExceptDailyAndCurrently, MetricSystem metricSystem = MetricSystem.AUTO);
  }
}
