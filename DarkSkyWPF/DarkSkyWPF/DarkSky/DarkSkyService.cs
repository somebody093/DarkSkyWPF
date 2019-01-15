using DarkSkyWPF.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkSkyWPF.DarkSky
{
    /// <summary>
    /// IDarkSkyService is responsible for the handling and wrapping the raw data coming from the DarkSky API
    /// </summary>
    public class DarkSkyService : IDarkSkyService
    {
        private IWeatherDataRetriever _weatherDataRetriever;

        public DarkSkyService(IWeatherDataRetriever weatherDataRetriever)
        {
            _weatherDataRetriever = ArgumentValidation.ThrowIfNull(weatherDataRetriever, nameof(weatherDataRetriever));
        }
    }
}
