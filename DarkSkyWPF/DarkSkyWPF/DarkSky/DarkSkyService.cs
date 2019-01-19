using DarkSkyWPF.DarkSky.JSONModels;
using DarkSkyWPF.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace DarkSkyWPF.DarkSky
{
  /// <summary>
  /// IDarkSkyService is responsible for handling service calls and wrapping the raw data coming from the DarkSky API
  /// </summary>
  public class DarkSkyService : IDarkSkyService
  {
    private IWeatherDataRetriever _weatherDataRetriever;
    private readonly Uri _darkSkyForecastBaseUrl;

    public DarkSkyService(IWeatherDataRetriever weatherDataRetriever)
    {
      _weatherDataRetriever = ArgumentValidation.ThrowIfNull(weatherDataRetriever, nameof(weatherDataRetriever));
      _darkSkyForecastBaseUrl = new Uri(ConfigurationManager.AppSettings.Get("darkSkyForecastBaseUrl"));
    }

    public async Task<WeatherDataRoot> GetWeatherDataForCity(City city, ExcludeParameter excludeParameter = ExcludeParameter.AllExceptDailyAndCurrently)
    {
      string relativeUrlWithQueryParams = BuildRelativeCityUrl(city, excludeParameter);

      string rawWeatherData = await _weatherDataRetriever.FetchWeatherData(new Uri(_darkSkyForecastBaseUrl, relativeUrlWithQueryParams));

      return ParseWeatherDataFromStringResult(rawWeatherData);
    }

    public async Task<IEnumerable<WeatherDataRoot>> GetWeatherDataForMultipleCities(IEnumerable<City> cities, ExcludeParameter excludeParameter = ExcludeParameter.AllExceptDailyAndCurrently)
    {
      IList<WeatherDataRoot> weatherDataList = new List<WeatherDataRoot>();

      foreach (City city in cities)
      {
        string relativeUrlWithQueryParams = BuildRelativeCityUrl(city, excludeParameter);
        string rawWeatherData = await _weatherDataRetriever.FetchWeatherData(new Uri(_darkSkyForecastBaseUrl, relativeUrlWithQueryParams));
        //todo: modify collection and store city names or modify json to already have city name in the data
        weatherDataList.Add(ParseWeatherDataFromStringResult(rawWeatherData));
      }
      return weatherDataList.ToArray();
    }

    private WeatherDataRoot ParseWeatherDataFromStringResult(string rawWeatherData)
    {
      //only separate line for breakpoint
      WeatherDataRoot weatherData = JsonConvert.DeserializeObject<WeatherDataRoot>(rawWeatherData);
      return weatherData;
    }

    private string BuildRelativeCityUrl(City city, ExcludeParameter excludeParameter)
    {
      string relativeUrlForCityWithoutQueryParams = city.Latitude + "," + city.Longitude;
      string excludeQueryParameter = MapExcludeToQueryParameter(excludeParameter);
      return relativeUrlForCityWithoutQueryParams + excludeQueryParameter;
    }

    private string MapExcludeToQueryParameter(ExcludeParameter excludeParameter)
    {
      switch (excludeParameter)
      {
        case ExcludeParameter.None:
          return string.Empty;
        case ExcludeParameter.Currently:
          return "?exclude=currently";
        case ExcludeParameter.Minutely:
          return "?exclude=minutely";
        case ExcludeParameter.Hourly:
          return "?exclude=hourly";
        case ExcludeParameter.Daily:
          return "?exclude=daily";
        case ExcludeParameter.Alerts:
          return "?exclude=alerts";
        case ExcludeParameter.Flags:
          return "?exclude=flags";
        case ExcludeParameter.CurrentlyAndMinutely:
          return "?exclude=currently,minutely";
        case ExcludeParameter.CurrentlyAndHourly:
          return "?exclude=currently,hourly";
        case ExcludeParameter.AlertsAndFlags:
          return "?exclude=alerts,flags";
        case ExcludeParameter.AllExceptDaily:
          return "?exclude=currently,minutely,hourly,alerts,flags";
        case ExcludeParameter.AllExceptDailyAndCurrently:
          return "?exclude=minutely,hourly,alerts,flags";
        default:
          return string.Empty;
      }
    }
  }
}
