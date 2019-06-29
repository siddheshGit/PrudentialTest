using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherServiceApplication
{
    public class WeatherFileModel
    {
        [JsonProperty (PropertyName ="City Name")]
        public string CityName { get; set; }

        [JsonProperty(PropertyName = "Date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "Weather Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "Temperature(Celsius)")]
        public double Temperature { get; set; }

        [JsonProperty(PropertyName = "Pressure(hPa)")]
        public int Pressure { get; set; }

        [JsonProperty(PropertyName = "Humidity(%)")]
        public int Humidity { get; set; }

        [JsonProperty(PropertyName = "Temperature Min(Celsius)")]
        public double Temp_min { get; set; }

        [JsonProperty(PropertyName = "Temperature Max(Celsius)")]
        public double Temp_max { get; set; }

        [JsonProperty(PropertyName = "WindSpeed(meter/sec)")]
        public double WindSpeed { get; set; }

        [JsonProperty(PropertyName = "Sunrise(DateTime)")]
        public DateTime Sunrise { get; set; }

        [JsonProperty(PropertyName = "Sunset(DateTime)")]
        public DateTime Sunset { get; set; }

        
    }
}
