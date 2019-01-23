using System.Configuration;

namespace DarkSkyWPF.Configuration
{
  /// <summary>
  /// Wrapper class around Microsoft Configuration's ConfigurationManager to be more testable
  /// </summary>
  public class DarkSkyConfigurationManager : IDarkSkyConfigurationManager
  {
    public string GetConfigFromAppSettings(string name)
    {
      return ConfigurationManager.AppSettings.Get(name);
    }
  }
}
