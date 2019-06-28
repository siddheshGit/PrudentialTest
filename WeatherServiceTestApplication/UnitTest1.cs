using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherServiceApplication;

namespace WeatherServiceTestApplication
{
    [TestClass]
    public class UnitTest1
    {
        WheatherService asa = new WheatherService();

        [TestMethod]
        public void TestFileReadMethod()
        {
            string fileName = "city.json";
            List<CityModel> CityList=asa.ReadCityJsonFile(fileName);
            int cityListCount = CityList.Count;

            bool test = cityListCount > 0;

            Assert.AreEqual(true,test);
        }
    }
}
