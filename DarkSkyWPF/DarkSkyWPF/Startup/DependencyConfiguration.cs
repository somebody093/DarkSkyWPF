using DarkSkyWPF.DarkSky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace DarkSkyWPF.Startup
{
  public static class DependencyConfiguration
  {
    public static IUnityContainer BuildUnityContainer()
    {
      IUnityContainer container = new UnityContainer();

      container.RegisterType<IWeatherDataRetriever, WeatherDataRetriever>();
      container.RegisterType<IDarkSkyService, DarkSkyService>();

      return container;
    }
  }
}
