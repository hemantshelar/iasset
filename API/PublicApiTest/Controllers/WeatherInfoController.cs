using iasset.GlobalWeatherProvider.Core.Interface;
using iasset.GlobalWeatherProvider.Core.Model;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Iasset.Weatherapi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/WeatherInfo")]
    public class WeatherInfoController : ApiController
    {
        IGlobalWeatherProvider _globalWeatherProvider = null;

        #region ctor
        public WeatherInfoController(IGlobalWeatherProvider _globalWeatherProvider)
        {
            this._globalWeatherProvider = _globalWeatherProvider;
        }
        #endregion

        #region GetCountries
        [HttpGet]
        [Route("getCountries")]
        public async Task<IHttpActionResult> GetCountries()
        {
            var countries = await Task.Run(() => _globalWeatherProvider.GetCountries());

            return Ok(countries);
        }
        #endregion

        #region GetCities
        [HttpGet]
        [Route("getCities")]
        public async Task<IHttpActionResult> GetCities(string country)
        {
            var countries = await Task.Run(() => _globalWeatherProvider.GetCities(country));
            return Ok(countries);
        }
        #endregion

        #region GetWeatherInfo
        [HttpGet]
        [Route("getWeatherInfo")]
        public async Task<IHttpActionResult> GetWeatherInfo(string country, string city)
        {
            var countries = await Task.Run(() => _globalWeatherProvider.GetWeather(new City { Name = city , Country = country }));
            return Ok(countries);
        }
        #endregion 
    }
}