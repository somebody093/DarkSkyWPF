# DarkSkyWPF

The DarkSkyWPF project is WPF XAML and MVVM-based interface for Dark Sky API.
It supports the following cities's daily and week-ahead weather information: Budapest, Luxembourg, Debrecen, Pécs, Vienna, Prague, München and Amsterdam.

...with Hungarian...
![Hungarian Application Interface Image](https://github.com/somebody093/DarkSkyWPF/blob/master/darkSkyHungarianNormal.JPG)


...and English language support:
![English Application Interface Image](https://github.com/somebody093/DarkSkyWPF/blob/master/darkSkyEnglishNormal.JPG)

It also signals when the DarkSky API connection is not available:
![Error in the connection Image](https://github.com/somebody093/DarkSkyWPF/blob/master/darkSkyWhenThereIsNoDataAvailable.JPG)


## Acknowledgments & Used software

* Unity container for dependency injection 
* MVVM Light's RelayCommand and INotifyPropertyChanged features
* JSON.NET serialization & deserialization
* @adamwhitcroft's Climacon images with my edits
* WPF Localize Extension
* log4net logging
* Nunit & Rhino Mocks for testing
