using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkSkyWPF.Services
{
  /// <summary>
  /// ICityService is responsible for providing the list of cities. If at a later phase handling cities would also be through a service e.g. the maps API, that also can be put here.
  /// </summary>
  public interface ICityService
  {
    IEnumerable<City> AvilableCities { get; }
  }
}
