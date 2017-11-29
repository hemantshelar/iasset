using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using iasset.GlobalWeatherProvider.Core.Interface;
using Iasset.Weatherapi.Controllers;
using System.Collections.Generic;
using iasset.GlobalWeatherProvider.Core.Model;
using System.Web.Http;
using System.Net;
using System.Web.Http.Results;

namespace iasset.test
{
    [TestClass]
    public class UnitTestWeatherInfoController
    {
        [TestMethod]
        [Owner("hemant")]
        public void Test_GetCountries_It_Should_Return_List_Of_all_countries()
        {
            //Arrange
            Mock<IGlobalWeatherProvider> mockedGlobalWeatherProvider = new Mock<IGlobalWeatherProvider>();

            List<Country> _mockedCountryList = new List<Country>();
            _mockedCountryList.Add(new Country { ID = "Australia" , Name = "Australia"});
            mockedGlobalWeatherProvider.Setup(x => x.GetCountries()).Returns(_mockedCountryList);
            WeatherInfoController objWeatherInfoController = new WeatherInfoController(mockedGlobalWeatherProvider.Object);

            //Act
            IHttpActionResult actionResult = objWeatherInfoController.GetCountries().Result;

            ///Assert            
            Assert.IsInstanceOfType(actionResult,typeof(OkNegotiatedContentResult<List<Country>>));
            Assert.IsNotNull(actionResult as OkNegotiatedContentResult<List<Country>>);

            OkNegotiatedContentResult<List<Country>> r = actionResult as OkNegotiatedContentResult<List<Country>>;
            Assert.AreNotEqual(0, r.Content.Count);
        }
    }
}
