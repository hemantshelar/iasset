using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iasset.GlobalWeatherProvider.Core;
using iasset.GlobalWeatherProvider.Core.Model;

namespace iasset.test
{
    [TestClass]
    public class UnitTestWebServicexGlobalWeatherProvider
    {
        #region GetCountry tests
        [TestMethod]
        [Owner("hemant")]
        public void TestGetCities_it_should_return_list_of_all_countries()
        {
            //Setup
            WebServicexGlobalWeatherProvider _provider = new WebServicexGlobalWeatherProvider();

            //Act
            var result = _provider.GetCountries();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(result.Count, 0);
        }
        #endregion

        #region City related test cases.
        [TestMethod]
        [Owner("hemant")]
        public void TestGetCities_It_Should_Return_List_Of_Cities_Correspondng_To_Country()
        {
            //Setup
            WebServicexGlobalWeatherProvider _provider = new WebServicexGlobalWeatherProvider();

            //Act
            var result = _provider.GetCities("Australia");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(result.Count, 0);

        }

        [TestMethod]
        [Owner("hemant")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetCities_It_Shoud_Throw_Argument_Exception_If_Parameter_Country_IsNull_Or_Empty()
        {
            //Setup
            WebServicexGlobalWeatherProvider _provider = new WebServicexGlobalWeatherProvider();

            //Act
            var result = _provider.GetCities("");         

        }

        [TestMethod]
        [Owner("hemant")]
        public void TestGetCities_It_Shoud_Return_Empty_List_If_We_Pass_Invalid_CountryName()
        {
            //Setup
            WebServicexGlobalWeatherProvider _provider = new WebServicexGlobalWeatherProvider();

            //Act
            var result = _provider.GetCities("Unknown Country");

            //Assert
            Assert.AreEqual(0, result.Count);
        }
        #endregion

        #region GetWeather tests.
        [TestMethod]
        [Owner("hemant")]
        [ExpectedException(typeof(Exception))]
        public void TestGetWeather_It_Shoud_Throw_Exception_If_Country_Is_Unknown()
        {
            //Setup
            WebServicexGlobalWeatherProvider _provider = new WebServicexGlobalWeatherProvider();
            City city = new City { Country = "Unknown", Name = "Sydney" };

            //Act
            var result = _provider.GetWeather(city);
        }

        [TestMethod]
        [Owner("hemant")]
        public void TestGetWeather_It_Shoud_Return_Weather_Parameters()
        {
            //Setup
            WebServicexGlobalWeatherProvider _provider = new WebServicexGlobalWeatherProvider();
            City city = new City { Country = "Australia", Name = "Sydney" };

            //Act
            var result = _provider.GetWeather(city);

            //Assert
            Assert.AreNotEqual(null, result);
        }
        #endregion
    }
}
