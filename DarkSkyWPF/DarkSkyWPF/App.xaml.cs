using DarkSkyWPF.Services.Cities;
using DarkSkyWPF.Services.DarkSky;
using DarkSkyWPF.Startup;
using DarkSkyWPF.ViewModels;
using System.Windows;
using Unity;

namespace DarkSkyWPF
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
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
