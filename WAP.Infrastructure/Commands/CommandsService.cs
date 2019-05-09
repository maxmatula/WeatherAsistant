using WAP.Infrastructure.Methods;
using System;
using System.Collections.Generic;
using WAP.DataAccess.DAL;
using WAP.DataAccess.Models;
using System.Linq;

namespace WAP.Infrastructure.Commands
{
    public class CommandsService
    {
        private readonly List<string> _commands = CommandsList.commands;

        public void CheckCommand(string command)
        {
            //Add new commands here as if statement

            if (command.ToLower().Equals("exit"))
            {
                Environment.Exit(0);
            }

            if (command.ToLower().Equals("clear database") || command.ToLower().Equals("cd"))
            {
                using (var db = new WapContext())
                {
                    var entries = db.QueryLogs.ToList();

                    Console.WriteLine("");
                    Console.WriteLine($"You have {entries.Count} entries in database!");
                    Console.WriteLine("Do you want to delete them?");
                    Console.WriteLine("y/n");
                    var decision = Console.ReadLine();

                    switch (decision)
                    {
                        case "n":
                            Console.WriteLine("Nothing changed!");
                            break;

                        case "y":
                            foreach(var entry in entries)
                            {
                                db.QueryLogs.Remove(entry); 
                            }
                            db.SaveChanges();
                            Console.WriteLine($"Deleted {entries.Count} entries. Database is clean!");
                            break;

                        default:
                            Console.WriteLine("Nothing changed!");
                            break;
                    }
                }
            }

            if (command.ToLower().Equals("--help"))
            {
                Console.WriteLine("");
                Console.WriteLine("== List of Commands ==");
                foreach (string com in _commands)
                {
                    Console.WriteLine($"{com}");
                }
            }

            if (command.ToLower().Equals("weather city") || command.ToLower().Equals("wc"))
            {
                var weatherMethods = new WeatherMethods();
                string city = "";
                decimal cityQueriesAll;
                decimal cityQueriesSingleMax = 0;

                using (var db = new WapContext())
                {
                    cityQueriesAll = db.QueryLogs.Count();
                    var queryMax = db.QueryLogs.GroupBy(x => x.CityName);

                    foreach (var c in queryMax)
                    {
                        var count = c.Count();
                        if (cityQueriesSingleMax < count)
                        {
                            cityQueriesSingleMax = count;
                            var que = c.Select(x => x.CityName);
                            city = que.First();
                        }
                    }
                }

                var qPercent = cityQueriesSingleMax / cityQueriesAll;
                qPercent *= 100;

                if (qPercent > 50 && !string.IsNullOrWhiteSpace(city) && cityQueriesAll > 3)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Would you like to check weather for {city}?");
                    Console.WriteLine("y/n ?");
                    var decision = Console.ReadLine();

                    switch (decision)
                    {
                        case "y":
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
                            weatherMethods.ShowWeatherData(city);
                            break;

                        case "n":
                            weatherMethods.GetWeatherDataByCity();
                            break;

                        default:
                            weatherMethods.GetWeatherDataByCity();
                            break;
                    }

                }
                else
                {
                    weatherMethods.GetWeatherDataByCity();
                }



            }


            if (command.ToLower().Equals("show queries") || command.ToLower().Equals("sq"))
            {
                List<QueryLog> queryList;

                using (var db = new WapContext())
                {
                    db.Database.EnsureCreated();
                    queryList = db.QueryLogs.Take(100).ToList();
                }

                if (!queryList.Any())
                {
                    Console.WriteLine("There is no Query Logs in database!");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Listing queries");
                    Console.WriteLine("===============");

                    foreach (var log in queryList)
                    {
                        Console.WriteLine($"Log id: {log.QueryLogId}, " +
                            $"City: {log.CityName}, Date: {log.QueryDate}");
                    }

                    Console.WriteLine("===============");
                }
            }

            if (command.ToLower().Equals("show queries analysis") || command.ToLower().Equals("sqa"))
            {
                string city = "";
                decimal cityQueriesAll;
                decimal cityQueriesSingleMax = 0;

                using (var db = new WapContext())
                {
                    cityQueriesAll = db.QueryLogs.Count();
                    var queryMax = db.QueryLogs.GroupBy(x => x.CityName);

                    if (cityQueriesAll > 0)
                    {
                        foreach (var c in queryMax)
                        {
                            var count = c.Count();
                            if (cityQueriesSingleMax < count)
                            {
                                cityQueriesSingleMax = count;
                                var que = c.Select(x => x.CityName);
                                city = que.First();
                            }
                        }
                    }

                }

                if (cityQueriesAll > 0)
                {
                    var qPercent = cityQueriesSingleMax / cityQueriesAll;
                    qPercent *= 100;
                    qPercent = Math.Round(qPercent, 3);

                    var crossEde = qPercent > 50;

                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("========== Prediction Algorythm Analysis Data ==========");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("");
                    Console.WriteLine($"All entries: {cityQueriesAll}");
                    Console.WriteLine($"Most common city: {city}");
                    Console.WriteLine($"{city} entries: {cityQueriesSingleMax}");
                    Console.WriteLine($"{city} percentage: {qPercent}%");
                    Console.WriteLine($"Algorithm prediction city edge value = 50%");
                    Console.WriteLine($"Algorithm prediction city cross edge value? = {crossEde}");
                    Console.WriteLine("");
                    Console.WriteLine("========================================================");

                }
                else
                {
                    Console.WriteLine("There is no Query Logs in database!");
                }
            }
        }
    }
}
