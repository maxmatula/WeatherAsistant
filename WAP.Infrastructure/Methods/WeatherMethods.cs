using WAP.DataAccess;
using System;
using WAP.DataAccess.DAL;
using WAP.DataAccess.Models;

namespace WAP.Infrastructure.Methods
{
    public class WeatherMethods
    {
        private readonly IDataAccessMain _dataRepo;

        public WeatherMethods()
        {
            _dataRepo = new DataAccessMain();
        }

        public void GetWeatherDataByCity()
        {
            Console.WriteLine("");
            Console.WriteLine("Please type City name..");
            Console.Write("City: ");
            var city = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(city))
            {
                Console.WriteLine("City name empty!");
                Console.WriteLine("Please type City name..");
                Console.Write("City: ");
                city = Console.ReadLine();
            }

            using (var db = new WapContext())
            {
                db.Database.EnsureCreated();
                var ql = new QueryLog
                {
                    CityName = city,
                    QueryDate = DateTime.Now
                };

                db.QueryLogs.Add(ql);
                db.SaveChanges();
            }

            ShowWeatherData(city);
        }


        public async void ShowWeatherData(string city)
        {
            var entity = await _dataRepo.RetrieveWeatherDataByCityAsync(city);

            if (entity != null && !String.IsNullOrWhiteSpace(entity.Name))
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("=================== Weather Info ===================");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("");
                Console.WriteLine($"Date: {DateTime.Now}");
                Console.WriteLine($"City: {entity.Name}");
                Console.WriteLine($"The weather outside is {entity.Weather[0].Main}");
                Console.WriteLine($"Temperature is {entity.Main.Temp} C degrees");
                Console.WriteLine($"The pressure is {entity.Main.Pressure} hPa");
                Console.WriteLine("");
                Console.WriteLine("====================================================");

            }
        }
    }
}
