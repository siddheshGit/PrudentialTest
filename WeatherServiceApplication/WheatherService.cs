using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherServiceApplication
{
    public class WheatherService
    {
        string url = string.Empty;
        string appID = ConfigurationManager.AppSettings["appID"];
        string units = ConfigurationManager.AppSettings["units"];
         /// <summary>
         /// Read json file from location
         /// </summary>
         /// <returns></returns>         
        public List<CityModel> ReadCityJsonFile()
        {
            Console.WriteLine("Reading city details file....");
            List<CityModel> cityList = new List<CityModel>();

            try
            {
                using (StreamReader reader = new StreamReader("City.json"))
                {
                    string cityData = reader.ReadToEnd();
                    cityList = JsonConvert.DeserializeObject<List<CityModel>>(cityData);
                }
                Console.WriteLine("File Read Completed");
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message.ToString());
            }

            return cityList;
        }


        public async Task<string> GetWeatherByCity(List<CityModel> cityList)
        {                                            
            List<WeatherFileModel> AllCityWeatherList = new List<WeatherFileModel>();
            string result= string.Empty;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    Console.WriteLine("Calling wheather Api");
                    foreach (CityModel item in cityList)
                    {
                        url = "http://api.openweathermap.org/data/2.5/weather?id=" + item.id + "&appid=" + appID+"&units="+units;
                        Task<HttpResponseMessage> response = httpClient.GetAsync(url);
                        string responseText = await response.Result.Content.ReadAsStringAsync();
                        CityWeather cityWeather = JsonConvert.DeserializeObject<CityWeather>(responseText);

                        WeatherFileModel fileModel = MapWeatherResponse(cityWeather);

                        result= CreateJsonFile(fileModel);
                    }

                }
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }

            return result;

        }

        public WeatherFileModel MapWeatherResponse(CityWeather cityWeatherResponse)
        {
            WeatherFileModel fileModel = new WeatherFileModel();

            fileModel.CityName = cityWeatherResponse.name;
            fileModel.Date = cityWeatherResponse.dt;
            fileModel.Description = cityWeatherResponse.weather.Select(d => d.description).FirstOrDefault();
            fileModel.Temperature = cityWeatherResponse.main.temp;
            fileModel.Pressure = cityWeatherResponse.main.pressure;
            fileModel.Humidity = cityWeatherResponse.main.humidity;
            fileModel.Temp_max = cityWeatherResponse.main.temp_max;
            fileModel.Temp_min = cityWeatherResponse.main.temp_min;
            fileModel.WindSpeed = cityWeatherResponse.wind.speed;
            fileModel.Sunrise = cityWeatherResponse.sys.sunrise;
            fileModel.Sunset = cityWeatherResponse.sys.sunset;

            return fileModel;
        }

        public string CreateJsonFile(WeatherFileModel fileModel)
        {
            string sucess;
            try
            {
                JObject obj = JObject.FromObject(new WeatherFileModel()
                {
                    
                    CityName = fileModel.CityName,
                    Date = fileModel.Date,
                    Description = fileModel.Description,
                    Temperature = fileModel.Temperature,
                    Pressure =fileModel.Pressure,
                    Humidity = fileModel.Humidity,
                    Temp_max = fileModel.Temp_max,
                    Temp_min = fileModel.Temp_min,
                    WindSpeed = fileModel.WindSpeed,
                    Sunrise = fileModel.Sunrise, 
                    Sunset = fileModel.Sunset, 
                });

                Directory.CreateDirectory(@"C:\Wheather Data");
                File.WriteAllText(@"C:\Wheather Data\" + fileModel.CityName + " " + fileModel.Date + ".txt", obj.ToString());

                sucess = "Wheather details file has been created and stored at C:Wheather Data folder";
            }
            catch (Exception ex)
            {
                sucess = ex.Message.ToString();
                throw;
            }
            return sucess;
        }

    }
}
