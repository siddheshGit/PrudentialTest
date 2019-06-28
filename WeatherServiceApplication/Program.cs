using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherServiceApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            WheatherService wheatherService = new WheatherService();
            string cityFileName = "city.json";

            List<CityModel>CityDetails=wheatherService.ReadCityJsonFile(cityFileName);

           wheatherService.GetWeatherByCity(CityDetails).Wait();

            Console.ReadKey();
        }
       
    }
}
