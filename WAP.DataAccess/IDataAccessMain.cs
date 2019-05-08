using WAP.DataAccess.Entities;
using System.Threading.Tasks;

namespace WAP.DataAccess
{
    public interface IDataAccessMain
    {
        Task<WeatherEntity> RetrieveWeatherDataByCityAsync(string city);
    }
}
