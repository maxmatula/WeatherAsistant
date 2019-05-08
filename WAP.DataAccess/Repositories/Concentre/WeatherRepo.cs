using WAP.DataAccess.Configuration;
using WAP.DataAccess.Entities;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WAP.DataAccess.Repositories.Abstract;

namespace WAP.DataAccess.Repositories.Concentre
{
    public class WeatherRepo : IWeatherRepo
    {
        private string _city;
        private string _appId;
        private string _appUrl;

        private string _url;

        public async Task<WeatherEntity> GetEntityFromJsonAsync(HttpResponseMessage message)
        {
            string json = await message.Content.ReadAsStringAsync();

            try
            {
                var result = JsonConvert.DeserializeObject<WeatherEntity>(json);
                return result;
            }
            catch (Exception e)
            {

                throw new JsonSerializationException("Cannot convert from entity", e);
            }
        }

        public async Task<HttpResponseMessage> GetHttpResponseMessageAsync()
        {
            var client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync(_url);

            if (!message.IsSuccessStatusCode)
            {
                Console.WriteLine("");
                Console.WriteLine("Cannot retrieve data from WeatherApi");
                Console.WriteLine("Weather api not responds or privided City does not exists!");
                Console.WriteLine("");
            }

            return message;
        }

        public void SetInputData(string city)
        {
            _city = city;
            _appId = DataAccessConfig.OpenWeatherMapAppId;
            _appUrl = DataAccessConfig.OpenWeatherMapUrl;

            _url = $"{_appUrl}?q={_city}&APPID={_appId}&units=metric";
        }

        public bool ValidateInputData()
        {
            if (string.IsNullOrWhiteSpace(_city)) { Console.WriteLine(""); Console.WriteLine("A City has to be provided!"); return false; }
            if (string.IsNullOrWhiteSpace(_appId)) { Console.WriteLine(""); Console.WriteLine("An AppId has to be provided!"); return false; }
            if (string.IsNullOrWhiteSpace(_appUrl)) { Console.WriteLine(""); Console.WriteLine("An AppUrl has to be provided!"); return false; }

            return true;
        }
    }
}
