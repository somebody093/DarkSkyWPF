using DarkSkyWPF.Services.Cities;
using DarkSkyWPF.Services.DarkSky.JSONModels;
using DarkSkyWPF.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace DarkSkyWPF.Services.DarkSky
{
  /// <summary>
  /// The DarkSkyService implementation is responsible for handling service calls and wrapping the raw data coming from the DarkSky API
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

    public async Task<WeatherDataRoot> GetWeatherDataForCity(City city, ExcludeParameter excludeParameter = ExcludeParameter.AllExceptDailyAndCurrently, MetricSystem metricSystem = MetricSystem.AUTO)
    {
      string relativeUrlWithQueryParams = BuildRelativeCityUrl(city, excludeParameter, metricSystem);

      string rawWeatherData = await _weatherDataRetriever.FetchWeatherData(new Uri(_darkSkyForecastBaseUrl, relativeUrlWithQueryParams));

      WeatherDataRoot weatherDataForCity = ParseWeatherDataFromStringResult(rawWeatherData);
      weatherDataForCity.CityName = city.Name;

      return weatherDataForCity;
    }

    public async Task<IEnumerable<WeatherDataRoot>> GetWeatherDataForMultipleCities(IEnumerable<City> cities, ExcludeParameter excludeParameter = ExcludeParameter.AllExceptDailyAndCurrently, MetricSystem metricSystem = MetricSystem.AUTO)
    {
      IList<WeatherDataRoot> weatherDataList = new List<WeatherDataRoot>();

      foreach (City city in cities)
      {
        string relativeUrlWithQueryParams = BuildRelativeCityUrl(city, excludeParameter, metricSystem);
        string rawWeatherData = await _weatherDataRetriever.FetchWeatherData(new Uri(_darkSkyForecastBaseUrl, relativeUrlWithQueryParams));

        WeatherDataRoot weatherDataForCity = ParseWeatherDataFromStringResult(rawWeatherData);
        weatherDataForCity.CityName = city.Name;
        weatherDataList.Add(weatherDataForCity);
      }
      return weatherDataList.ToArray();
    }

    private WeatherDataRoot ParseWeatherDataFromStringResult(string rawWeatherData)
    {
      return JsonConvert.DeserializeObject<WeatherDataRoot>(rawWeatherData);
    }

    private string BuildRelativeCityUrl(City city, ExcludeParameter excludeParameter, MetricSystem metricSystem)
    {
      string relativeUrlForCityWithoutQueryParams = city.Latitude + "," + city.Longitude;
      string excludeQueryParameter = MapExcludeToQueryParameter(excludeParameter);
      string metricQueryParametere = MapMetricSystemToQueryParameter(metricSystem);
      return relativeUrlForCityWithoutQueryParams + "?" + excludeQueryParameter + "&" + metricQueryParametere;
    }

    private string MapExcludeToQueryParameter(ExcludeParameter excludeParameter)
    {
      switch (excludeParameter)
      {
        case ExcludeParameter.None:
          return string.Empty;
        case ExcludeParameter.Minutely:
          return "exclude=minutely";
        case ExcludeParameter.Hourly:
          return "exclude=hourly";
        case ExcludeParameter.Alerts:
          return "exclude=alerts";
        case ExcludeParameter.Flags:
          return "exclude=flags";;
        case ExcludeParameter.AlertsAndFlags:
          return "exclude=alerts,flags";
        case ExcludeParameter.AllExceptDailyAndCurrently:
          return "exclude=minutely,hourly,alerts,flags";
        default:
          return string.Empty;
      }
    }

    private string MapMetricSystemToQueryParameter(MetricSystem metricSystem)
    {
      switch (metricSystem)
      {
        case MetricSystem.AUTO:
          return "units=auto";
        case MetricSystem.SI:
          return "units=si";
        case MetricSystem.US:
          return "units=us";
        default:
          return "units=auto";
      }
    }
  }
}
