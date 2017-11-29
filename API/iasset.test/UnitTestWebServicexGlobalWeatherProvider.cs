using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iasset.GlobalWeatherProvider.Core;
using iasset.GlobalWeatherProvider.Core.Model;
using System.Text;

namespace iasset.test
{
    [TestClass]
    public class UnitTestWebServicexGlobalWeatherProvider
    {
        [TestMethod]
        [Owner("hemant")]
        public void TestGetCities_ItShouldReturnListOfCitiesCorrespondngToCountry()
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
        public void TestGetCities_ItShoudThrowArgumentExceptionIfParameterCountryIsNullOrEmpty()
        {
            //Setup
            WebServicexGlobalWeatherProvider _provider = new WebServicexGlobalWeatherProvider();

            //Act
            var result = _provider.GetCities("");         

        }

        [TestMethod]
        [Owner("hemant")]
        public void TestGetCities_ItShoudReturnEmptyListIfWePassInvalidCountryName()
        {
            //Setup
            WebServicexGlobalWeatherProvider _provider = new WebServicexGlobalWeatherProvider();

            //Act
            var result = _provider.GetCities("Unknown Country");

            //Assert
            Assert.AreEqual(0, result.Count);
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

        [TestMethod]
        [Owner("hemant")]
        public void TestGetCities_It_Should_Return_List_Of_Cities_Correspondng_To_Country1()
        {
            //Setup
            WebServicexGlobalWeatherProvider _provider = new WebServicexGlobalWeatherProvider();

            //Act
            var result = _provider.GetCities("Australia");

            //Assert
            foreach (var item in result)
            {
                _provider.GetWeather(new City { Name = item.Name , Country = item.Country });

            }


        }

        [TestMethod]
        [Owner("hemant")]
        public void TestGetWeather_It_Shoud_Throw_ArgumentException_If_City_Parameter_is_null()
        {
            //Setup
            WebServicexGlobalWeatherProvider _provider = new WebServicexGlobalWeatherProvider();

            //Act
            var cities = _provider.GetCities("Australia");

            StringBuilder sb = new StringBuilder();
            int nIndex = 0;
            foreach (var item in cities)
            {
                sb.Append(nIndex.ToString() + "-" + item.Name + "-" + item.Country + "\r\n");
                nIndex++;
            }

            var result = _provider.GetWeather(cities[58]);


            



        }

    }
}
