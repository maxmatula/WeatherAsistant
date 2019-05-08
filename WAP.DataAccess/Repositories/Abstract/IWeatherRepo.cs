using WAP.DataAccess.Entities;
using System.Net.Http;
using System.Threading.Tasks;

namespace WAP.DataAccess.Repositories.Abstract
{
    public interface IWeatherRepo
    {
        void SetInputData(string city);
        bool ValidateInputData();
        Task<HttpResponseMessage> GetHttpResponseMessageAsync();
        Task<WeatherEntity> GetEntityFromJsonAsync(HttpResponseMessage message);
    }
}
