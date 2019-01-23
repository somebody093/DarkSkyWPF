using DarkSkyWPF.Services.Cities;
using DarkSkyWPF.Services.DarkSky.JSONModels;
using DarkSkyWPF.Validation;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DarkSkyWPF.Services.DarkSky
{
  /// <summary>
  /// The DarkSkyService implementation is responsible for handling service calls and wrapping the raw data coming from the DarkSky API
  /// </summary>
  public class DarkSkyService : IDarkSkyService
  {
    private static readonly ILog Logger = LogManager.GetLogger(typeof(DarkSkyService));

    private IWeatherDataRetriever _weatherDataRetriever;
    private readonly Uri _darkSkyForecastBaseUrl;

    public DarkSkyService(IWeatherDataRetriever weatherDataRetriever)
    {
      _weatherDataRetriever = ArgumentValidation.ThrowIfNull(weatherDataRetriever, nameof(weatherDataRetriever));
      _darkSkyForecastBaseUrl = new Uri(ConfigurationManager.AppSettings.Get("darkSkyForecastBaseUrl"));
    }

    public async Task<WeatherDataRoot> GetWeatherDataForCity(City city, ExcludeParameter excludeParameter = ExcludeParameter.AllExceptDailyAndCurrently, MetricSystem metricSystem = MetricSystem.AUTO)
    {
      return await ProcessCityWeatherDataQuery(city, excludeParameter, metricSystem);
    }

    public async Task<IEnumerable<WeatherDataRoot>> GetWeatherDataForMultipleCities(IEnumerable<City> cities, ExcludeParameter excludeParameter = ExcludeParameter.AllExceptDailyAndCurrently, MetricSystem metricSystem = MetricSystem.AUTO)
    {
      IList<WeatherDataRoot> weatherDataList = new List<WeatherDataRoot>();

      foreach (City city in cities)
      {
        WeatherDataRoot weatherDataForCity = await ProcessCityWeatherDataQuery(city, excludeParameter, metricSystem);

        weatherDataList.Add(weatherDataForCity);
      }
      return weatherDataList.ToArray();
    }

    private async Task<WeatherDataRoot> ProcessCityWeatherDataQuery(City city, ExcludeParameter excludeParameter, MetricSystem metricSystem)
    {
      string relativeUrlWithQueryParams = BuildRelativeCityUrl(ArgumentValidation.ThrowIfNull(city, nameof(city)), excludeParameter, metricSystem);

      Uri fullUrl;
      string rawWeatherData;

      try
      {
        fullUrl = new Uri(_darkSkyForecastBaseUrl, relativeUrlWithQueryParams);
        rawWeatherData = await _weatherDataRetriever.FetchWeatherData(fullUrl);
      }
      catch (UriFormatException ex)
      {
        Logger.Error("The DarkSky Forecast Request URL used has invalid format.", ex);
        throw ex;
      }
      catch (HttpRequestException ex)
      {
        Logger.Error("The DarkSky Forecast Request failed due to an underlying issue e.g. network connectivity.", ex);
        throw ex;
      }
      catch (ArgumentOutOfRangeException ex)
      {
        Logger.Error("The DarkSky Forecast Base URL processing was unsuccessful.", ex);
        throw ex;
      }
      catch (ArgumentNullException ex)
      {
        Logger.Error("The DarkSky Forecast Base URL was null.", ex);
        throw ex;
      }

      if (Logger.IsInfoEnabled)
      {
        Logger.Info($"Successful weather data retrieval for {city.Name} from URL: {fullUrl}");
      }

      WeatherDataRoot weatherDataForCity = ParseWeatherDataFromStringResult(rawWeatherData);
      if (weatherDataForCity != null)
      {
        weatherDataForCity.CityName = city.Name;
      }
      return weatherDataForCity;
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
