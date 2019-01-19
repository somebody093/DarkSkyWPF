using DarkSkyWPF.DarkSky;
using DarkSkyWPF.DarkSky.JSONModels;
using DarkSkyWPF.Startup;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace DarkSkyWPF
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    protected async override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      IUnityContainer container = DependencyConfiguration.BuildUnityContainer();

      //for testing before building a UI
      IDarkSkyService darkSkyServiceForTest = container.Resolve<DarkSkyService>();
      City budapest = new City() { Name = "Budapest", Latitude = "47.4984", Longitude = "19.0405"};
      WeatherDataRoot weatherData = await darkSkyServiceForTest.GetWeatherDataForCity(budapest);
    }
  }
}
