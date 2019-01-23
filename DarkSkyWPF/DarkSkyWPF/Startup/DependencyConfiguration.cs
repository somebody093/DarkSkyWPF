using DarkSkyWPF.Configuration;
using DarkSkyWPF.Services.Cities;
using DarkSkyWPF.Services.DarkSky;
using Unity;

namespace DarkSkyWPF.Startup
{
  public static class DependencyConfiguration
  {
    public static IUnityContainer BuildUnityContainer()
    {
      IUnityContainer container = new UnityContainer();

      container.RegisterType<IWeatherDataRetriever, WeatherDataRetriever>();
      container.RegisterType<IDarkSkyConfigurationManager, DarkSkyConfigurationManager>();
      container.RegisterSingleton<IDarkSkyService, DarkSkyService>();
      container.RegisterSingleton<ICityService, CityService>();

      return container;
    }
  }
}
