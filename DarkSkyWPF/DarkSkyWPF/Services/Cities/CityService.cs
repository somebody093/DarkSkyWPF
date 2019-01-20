using System.Collections.Generic;

namespace DarkSkyWPF.Services.Cities
{
  /// <summary>
  /// The CityService implementation is responsible for providing the list of cities. If at a later phase handling cities would also be through a service e.g. the maps API, that also can be put here.
  /// </summary>
  public class CityService : ICityService
  {
    private List<City> _availableStarterCities;

    public IEnumerable<City> AvilableCities => _availableStarterCities ?? PopulateCities();

    private IEnumerable<City> PopulateCities()
    {
      _availableStarterCities = new List<City>();
      _availableStarterCities.Add(new City() { Name = "Budapest", Latitude = "47.4984", Longitude = "19.0405" });
      _availableStarterCities.Add(new City() { Name = "Luxembourg", Latitude = "49.6113", Longitude = "6.1298" });
      _availableStarterCities.Add(new City() { Name = "Debrecen", Latitude = "47.5314", Longitude = "21.626" });
      _availableStarterCities.Add(new City() { Name = "Pécs", Latitude = "46.0763", Longitude = "18.2281" });
      _availableStarterCities.Add(new City() { Name = "Wienna", Latitude = "48.2084", Longitude = "16.3725" });
      _availableStarterCities.Add(new City() { Name = "Prague", Latitude = "50.0875", Longitude = "14.4213" });
      _availableStarterCities.Add(new City() { Name = "München", Latitude = "48.1371", Longitude = "11.5754" });
      _availableStarterCities.Add(new City() { Name = "Amsterdam", Latitude = "52.3745", Longitude = "4.898" });
      return _availableStarterCities.ToArray();
    }
  }
}
