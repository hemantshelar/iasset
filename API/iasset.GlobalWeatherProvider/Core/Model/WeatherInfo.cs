using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iasset.GlobalWeatherProvider.Core.Model
{
    public class WeatherInfo
    {
        /// <summary>
        /// Not sure what is significance of Time field. 
        /// So assigning current date/time
        /// </summary>
        public WeatherInfo()
        {
            Time = DateTime.Now;
        }

        public double WndDeg { get; set; }          //Wind.deg
        public double WindSpeed { get; set; }       //Wind.speed

        public double LocationLon { get; set; }     //coord.log
        public double LocationLat { get; set; }     //coord.lat

        public double Visibility { get; set; }      //RootObject.visibility
        public string SkyCondition { get; set; }    //Weather.description

        public double Temperature { get; set; }     //Main.temp

        public string DewPoint { get; set; }        //Not found

        public double Humidity { get; set; }        //Main.humidity
        public double Pressure { get; set; }        //Main.Pressure
        public DateTime Time { get; set; }
    }
}
