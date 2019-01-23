using DarkSkyWPF.Configuration;
using DarkSkyWPF.Services.Cities;
using DarkSkyWPF.Services.DarkSky;
using DarkSkyWPF.Services.DarkSky.JSONModels;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Threading.Tasks;

namespace DarkSkyWPF.Tests.Services
{
  [TestFixture]
  public class DarkSkyServiceTests
  {
    private IWeatherDataRetriever _weatherDataRetriever;
    private IWeatherDataRetriever _weatherDataRetrieverToReturnEmpty;

    private IDarkSkyConfigurationManager _darkSkyConfigurationManager;
    private IDarkSkyConfigurationManager _darkSkyConfigurationManagerThatReturnsNull;
    private IDarkSkyConfigurationManager _darkSkyConfigurationManagerThatReturnsWrongFormat;
    private IDarkSkyService _darkSkyService;
    private string _configKeyName = "darkSkyForecastBaseUrl";
    private string _baseUrl = "https://api.darksky.net/forecast/770b4eb08f72e19cb01a6211a253cab0/";
    private Task<string> _mockEmptyResponseTask;
    private Task<string> _mockNormalDarkSkyResponseTask;

    private City city = new City() { Name = "Budapest", Latitude = "47.4984", Longitude = "19.0405" };
    private string _exampleResponse = "{\"latitude\":47.4984,\"longitude\":19.0405,\"timezone\":\"Europe/Budapest\",\"currently\":{\"time\":1548284594,\"summary\":\"Overcast\",\"icon\":\"cloudy\",\"precipIntensity\":0.0178,\"precipProbability\":0.03,\"precipType\":\"snow\",\"temperature\":-1.73,\"apparentTemperature\":-4.64,\"humidity\":0.96,\"pressure\":1002.75,\"windSpeed\":2.14,\"uvIndex\":0},\"offset\":1}";


    [OneTimeSetUp]
    public void SetUpFixture()
    {
      _weatherDataRetriever = MockRepository.GenerateMock<IWeatherDataRetriever>();
      _weatherDataRetrieverToReturnEmpty = MockRepository.GenerateMock<IWeatherDataRetriever>();

      _darkSkyConfigurationManager = MockRepository.GenerateMock<IDarkSkyConfigurationManager>();
      _darkSkyConfigurationManager.Expect(configurationManager => configurationManager.GetConfigFromAppSettings(_configKeyName)).Return(_baseUrl);

      _darkSkyConfigurationManagerThatReturnsNull = MockRepository.GenerateMock<IDarkSkyConfigurationManager>();
      _darkSkyConfigurationManagerThatReturnsNull.Expect(configurationManager => configurationManager.GetConfigFromAppSettings(_configKeyName)).Return(null);

      _darkSkyConfigurationManagerThatReturnsWrongFormat = MockRepository.GenerateMock<IDarkSkyConfigurationManager>();
      _darkSkyConfigurationManagerThatReturnsWrongFormat.Expect(configurationManager => configurationManager.GetConfigFromAppSettings(_configKeyName)).Return("CLEARLY_NOT_AN_URL");

      _mockEmptyResponseTask = Task<string>.Factory.StartNew(() => null);
      _mockNormalDarkSkyResponseTask = Task<string>.Factory.StartNew(() => _exampleResponse);

      _weatherDataRetriever.Expect(retriever => retriever.FetchWeatherData(Arg<Uri>.Is.Anything)).Return(_mockNormalDarkSkyResponseTask);
      _weatherDataRetrieverToReturnEmpty.Expect(retriever => retriever.FetchWeatherData(Arg<Uri>.Is.Anything)).Return(_mockEmptyResponseTask);
    }

    [Test]
    public void DarkSkyService_Initializes_WhenAllParametersAreSupplied()
    {
      _darkSkyService = new DarkSkyService(_weatherDataRetriever, _darkSkyConfigurationManager);
      Assert.IsNotNull(_darkSkyService);
      Assert.IsInstanceOf<DarkSkyService>(_darkSkyService);
      Assert.IsInstanceOf<IDarkSkyService>(_darkSkyService);
    }

    [Test]
    public void DarkSkyService_Throws_WhenWeatherDataRetrieverIsNull()
    {
      Assert.Throws<ArgumentNullException>(() => new DarkSkyService(null, _darkSkyConfigurationManager));
    }

    [Test]
    public void DarkSkyService_Throws_WhenDarkSkyConfigurationManagerIsNull()
    {
      Assert.Throws<ArgumentNullException>(() => new DarkSkyService(_weatherDataRetriever, null));
    }

    [Test]
    public void DarkSkyService_Throws_WhenTheConfigStringIsWrong()
    {
      Assert.Throws<ArgumentNullException>(() => new DarkSkyService(_weatherDataRetriever, _darkSkyConfigurationManagerThatReturnsNull));
      Assert.Throws<UriFormatException>(() => new DarkSkyService(_weatherDataRetriever, _darkSkyConfigurationManagerThatReturnsWrongFormat));
    }

    [Test]
    public void GetWeatherDataForCity_Throws_WhenCityIsNull()
    {
      _darkSkyService = new DarkSkyService(_weatherDataRetriever, _darkSkyConfigurationManager);
      Assert.ThrowsAsync<ArgumentNullException>(() => _darkSkyService.GetWeatherDataForCity(null));
    }

    [Test]
    public void GetWeatherDataForCity_Throws_WhenTheRawResponseIsNull()
    {
      _darkSkyService = new DarkSkyService(_weatherDataRetrieverToReturnEmpty, _darkSkyConfigurationManager);
      Assert.ThrowsAsync<ArgumentNullException>(() => _darkSkyService.GetWeatherDataForCity(city));
    }

    [Test]
    public async Task GetWeatherDataForCity_SuccessfullyParses_NormalJSONString()
    {
      _darkSkyService = new DarkSkyService(_weatherDataRetriever, _darkSkyConfigurationManager);
      WeatherDataRoot weatherData = await _darkSkyService.GetWeatherDataForCity(city);
      Assert.IsNotNull(weatherData);
      Assert.IsNotNull(weatherData.CurrentWeather);
      Assert.AreEqual(weatherData.CurrentWeather.Temperature, -1.73);
      Assert.AreEqual(weatherData.CurrentWeather.Icon, IconValue.CLOUDY);
      Assert.AreEqual(weatherData.CurrentWeather.Humidity, 0.96);
    }
  }
}
