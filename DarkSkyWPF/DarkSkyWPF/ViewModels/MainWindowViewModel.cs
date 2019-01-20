using DarkSkyWPF.Services;
using DarkSkyWPF.Services.JSONModels;
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
    private IEnumerable<WeatherDataRoot> _availableWeatherInfo;
    private ObservableCollection<City> _requiredListOfCities;

    private RelayCommand _loadDataCommand;


    public MainWindowViewModel(IDarkSkyService darkSkyService, ICityService cityService)
    {
      _cityService = ArgumentValidation.ThrowIfNull<ICityService>(cityService, nameof(cityService));
      _darkSkyService = ArgumentValidation.ThrowIfNull<IDarkSkyService>(darkSkyService, nameof(darkSkyService));
    }

    public ObservableCollection<City> RequiredListOfCities
    {
      get
      {
        return _requiredListOfCities;
      }
      private set
      {
        if (_requiredListOfCities == value)
        {
          return;
        }

        _requiredListOfCities = value;
        RaisePropertyChanged(nameof(RequiredListOfCities));
      }
    }

    public RelayCommand LoadDataCommand => _loadDataCommand ?? (_loadDataCommand = new RelayCommand(() => LoadData()));

    internal async Task LoadData()
    {
      try
      {
        RequiredListOfCities = new ObservableCollection<City>(_cityService.AvilableCities);
        _availableWeatherInfo = await _darkSkyService.GetWeatherDataForMultipleCities(_requiredListOfCities);
      }
      catch (Exception)
      {
        //todo: log/error message on ui or sg.
      }     
    }
  }
}
