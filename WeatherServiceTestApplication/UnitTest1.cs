using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherServiceApplication;

namespace WeatherServiceTestApplication
{
    [TestClass]
    public class UnitTest1
    {
        WheatherService asa = new WheatherService();
        List<CityModel> CityList;

        [TestMethod]
        public void TestFileReadMethod()
        {
            string fileName = "city.json";
            CityList=asa.ReadCityJsonFile(fileName);
            int cityListCount = CityList.Count;

            bool test = cityListCount > 0;

            Assert.AreEqual(true,test);
        }


        [TestMethod]
        public async Task TestGetWeatherByCityMethod()
        {
                      
            var result =   await asa.GetWeatherByCity(CityList);
            Assert.IsNotNull(result, "error");
            Console.Write(result);
     

        }
    }
}
