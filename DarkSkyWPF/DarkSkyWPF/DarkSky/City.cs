namespace DarkSkyWPF.DarkSky
{
  /// <summary>
  /// Model class for City objects. A new DarkSky Forecast Request can be created via the city's Latitude and Longitude properties.
  /// </summary>
  public class City
  {
    public string Name { get; set; }

    //rethink type if used other than in urls
    public string Latitude { get; set; }

    public string Longitude { get; set; }
  }
}
