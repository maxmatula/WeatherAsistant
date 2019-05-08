using System;
using System.Collections.Generic;
using System.Text;

namespace WAP.DataAccess.Entities.WeatherData
{
    public class Sys
    {
        public int Type { get; set; }
        public int Id { get; set; }
        public float Message { get; set; }
        public string Country { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
    }
}
