using WAP.DataAccess.Entities;
using WAP.DataAccess.Repositories.Abstract;
using WAP.DataAccess.Repositories.Concentre;
using System;
using System.Threading.Tasks;

namespace WAP.DataAccess
{
    public class DataAccessMain : IDataAccessMain
    {
        private IWeatherRepo _weatherRepo;

        public DataAccessMain()
        {
            _weatherRepo = new WeatherRepo();
        }

        public async Task<WeatherEntity> RetrieveWeatherDataByCityAsync(string city)
        {
            _weatherRepo.SetInputData(city);
            var valid = _weatherRepo.ValidateInputData();

            if (valid)
            {
                var message = await _weatherRepo.GetHttpResponseMessageAsync();
                var result = await _weatherRepo.GetEntityFromJsonAsync(message);

                return result;
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Error: Input data not valid!");
                Console.WriteLine("Exiting weather service!");
                Console.WriteLine("-.-");
                Console.WriteLine("Exited with error code: 789");
                Console.WriteLine("");
                var w = new WeatherEntity();
                return w;
            }
        }
    }
}
