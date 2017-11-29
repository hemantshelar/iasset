using iasset.GlobalWeatherProvider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iasset.GlobalWeatherProvider.Core.Model;
using iasset.GlobalWeatherProvider.webservicex.globalweather;
using System.Xml;
using System.Xml.Linq;
using System.Net.Http;
using System.Configuration;

namespace iasset.GlobalWeatherProvider.Core
{
    #region Internal members.
    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public double deg { get; set; }
    }

    public class Clouds
    {
        public double all { get; set; }
    }

    public class Sys
    {
        public double type { get; set; }
        public double id { get; set; }
        public double message { get; set; }
        public string country { get; set; }
        public double sunrise { get; set; }
        public double sunset { get; set; }
    }

    public class RootObject
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public double visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public double dt { get; set; }
        public Sys sys { get; set; }
        public double id { get; set; }
        public string name { get; set; }
        public double cod { get; set; }
    }
    #endregion

    public class WebServicexGlobalWeatherProvider : IGlobalWeatherProvider
    {
        GlobalWeatherSoapClient _gwsc = new GlobalWeatherSoapClient();
        
        public List<City> GetCities(string country)
        {
            if ( string.IsNullOrEmpty(country))
            {
                throw new ArgumentException("Parameter country can not be null or empty.");
            }

            List<City> cities = new List<City>();
           
            var result = _gwsc.GetCitiesByCountry(country);
            
            #region Extract Cities out of API result.

            XDocument xmlDocument = XDocument.Parse(result);
            IEnumerable<XElement> elements = xmlDocument.Elements("NewDataSet");
            foreach (var item in elements.Elements())
            {
                cities.Add(new City
                {
                    Name = item.Element("City").Value,
                    Country = country
                });
            }
            #endregion 

            return cities;
        }

        public List<Country> GetCountries()
        {
            List<Country> _list = new List<Country>();

            _list.Add(new Country { ID = "Australia", Name = "Australia"  });
            _list.Add(new Country { ID = "India", Name = "India" });
            return _list;
        }

        public WeatherInfo GetWeather(City city)
        {
            #region Validations
            if ( city == null)
            {
                throw new ArgumentException("City object can not be null");
            }
            if ( string.IsNullOrEmpty(city.Country))
            {
                throw new ArgumentException("Country name can not be null or empty");
            }
            if ( string.IsNullOrEmpty(city.Name))
            {
                throw new ArgumentException("City name can not be null or empty.");
            }
            #endregion  

            #region Fetch API cnofiguration data.
            var endpoint = ConfigurationManager.AppSettings["WEATHER_SERVICE_ENDPOINT"];
            var apiKey = ConfigurationManager.AppSettings["OPEN_WEATHER_MAP_API_KEY"];
            endpoint = string.Format("{0}?q={1},{2}&APPID={3}",endpoint,city.Name,city.Country,apiKey);
            #endregion

            #region API call
            HttpClient client = new HttpClient();
           
            client.BaseAddress = new Uri(endpoint);
            var r = client.GetAsync(endpoint).Result;

            if ( r.IsSuccessStatusCode == false)
            {                
                string message = string.Format("Can not fetch data for country - {0} and city - {1}.  Status code : {2}",city.Country,city.Name,r.StatusCode);
                throw new Exception(message);
            }

            var jsonResponse = r.Content.ReadAsStringAsync().Result;
            var jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(jsonResponse);
            #endregion

            #region Populate WeatherInfo object
            var result = new WeatherInfo();
            result.WndDeg = jsonObject.wind.deg;
            result.WindSpeed = jsonObject.wind.speed;
            result.LocationLon = jsonObject.coord.lon;
            result.LocationLat = jsonObject.coord.lat;

            result.Visibility = jsonObject.visibility;
            result.SkyCondition = jsonObject.weather[0].description;
            result.Temperature = jsonObject.main.temp;
            result.DewPoint = "Not found";
            result.Humidity = jsonObject.main.humidity;
            result.Pressure = jsonObject.main.pressure;
            #endregion  

            return result;
        }
    }
}
