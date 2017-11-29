using iasset.GlobalWeatherProvider.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iasset.GlobalWeatherProvider.Core.Interface
{
    public interface IGlobalWeatherProvider
    {
        List<Country> GetCountries();
        List<City> GetCities(string country);
        WeatherInfo GetWeather(City city);
    }
}
