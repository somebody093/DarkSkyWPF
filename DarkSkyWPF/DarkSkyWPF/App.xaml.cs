using DarkSkyWPF.Services;
using DarkSkyWPF.Services.JSONModels;
using DarkSkyWPF.Startup;
using DarkSkyWPF.ViewModels;
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

      IDarkSkyService darkSkyService = container.Resolve<DarkSkyService>();
      ICityService cityService = container.Resolve<CityService>();
      MainWindow mainWindow = new MainWindow(new MainWindowViewModel(darkSkyService, cityService));

      mainWindow.Show();
    }
  }
}
