using DarkSkyWPF.Services.Cities;
using DarkSkyWPF.Services.DarkSky;
using DarkSkyWPF.Services.DarkSky.JSONModels;
using DarkSkyWPF.Validation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DarkSkyWPF.ViewModels
{
  public class MainWindowViewModel : ViewModelBase
  {
    private readonly ICityService _cityService;
    private readonly IDarkSkyService _darkSkyService;
    private ObservableCollection<WeatherDataRoot> _availableWeatherInfo;
    private IEnumerable<City> _requiredListOfCities;

    private RelayCommand _loadDataCommand;


    public MainWindowViewModel(IDarkSkyService darkSkyService, ICityService cityService)
    {
      _cityService = ArgumentValidation.ThrowIfNull<ICityService>(cityService, nameof(cityService));
      _darkSkyService = ArgumentValidation.ThrowIfNull<IDarkSkyService>(darkSkyService, nameof(darkSkyService));
      _availableWeatherInfo = new ObservableCollection<WeatherDataRoot>();
    }

    public ObservableCollection<WeatherDataRoot> AvailableWeatherInfo
    {
      get
      {
        return _availableWeatherInfo;
      }
      private set
      {
        if (_availableWeatherInfo == value)
        {
          return;
        }

        _availableWeatherInfo = value;
        RaisePropertyChanged(nameof(AvailableWeatherInfo));
      }
    }

    public RelayCommand LoadDataCommand => _loadDataCommand ?? (_loadDataCommand = new RelayCommand(() => LoadData()));

    private async Task LoadData()
    {
      try
      {
        if (_requiredListOfCities != null)
        {
          return;
        }

        _requiredListOfCities = _cityService.AvilableCities;
        //could be split to different chunks as well like 2 each or sg.
        foreach (City city in _requiredListOfCities)
        {
          WeatherDataRoot weatherInfo = await _darkSkyService.GetWeatherDataForCity(city);
          AvailableWeatherInfo.Add(weatherInfo);
        }

        //testing what is quicker in terms of loading data
        //IEnumerable<WeatherDataRoot> availableWeatherInfo  = await _darkSkyService.GetWeatherDataForMultipleCities(_requiredListOfCities);
        //AvailableWeatherInfo = new ObservableCollection<WeatherDataRoot>(availableWeatherInfo);
      }
      catch (Exception)
      {
        //todo: log/error message on ui or sg.
      }     
    }
  }
}
