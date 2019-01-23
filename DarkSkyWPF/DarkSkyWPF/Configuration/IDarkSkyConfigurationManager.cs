namespace DarkSkyWPF.Configuration
{
  /// <summary>
  /// Wrapper interface around Microsoft Configuration's ConfigurationManager to be more testable
  /// </summary>
  public interface IDarkSkyConfigurationManager
  {
    string GetConfigFromAppSettings(string name);
  }
}
