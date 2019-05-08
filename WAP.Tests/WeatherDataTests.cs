using WAP.DataAccess;
using WAP.DataAccess.Entities;
using WAP.DataAccess.Repositories.Abstract;
using WAP.DataAccess.Repositories.Concentre;
using System;
using System.Threading.Tasks;
using Xunit;

namespace WAP.Tests
{
    public class WeatherDataTests
    {
        private readonly IDataAccessMain _dataAccesService;
        private readonly IWeatherRepo _weatherRepo;

        public WeatherDataTests()
        {
            _dataAccesService = new DataAccessMain();
            _weatherRepo = new WeatherRepo();
        }

        [Fact]
        public async Task Can_Retrieve_Weather_Data()
        {
            //Arrange
            string city = "Rzeszow";
            WeatherEntity entity = null;

            //Act
            entity = await _dataAccesService.RetrieveWeatherDataByCityAsync(city);

            //Assert
            Assert.NotNull(entity);
        }

        [Fact]
        public async Task Retrieved_Object_Is_WeatherEntity_Type()
        {
            //Arrange
            string city = "Rzeszow";

            //Act
            var entity = await _dataAccesService.RetrieveWeatherDataByCityAsync(city);

            //Assert
            Assert.IsType<WeatherEntity>(entity);
        }

        [Fact]
        public async Task Empty_City_Return_Empty_Entity()
        {
            //Arrange
            string city = "";

            //Act
            var entity = await _dataAccesService.RetrieveWeatherDataByCityAsync(city);

            //Assert
            Assert.True(String.IsNullOrWhiteSpace(entity.Name));
        }

        [Fact]
        public async Task Wrong_Typed_City_Return_Empty_Entity()
        {
            //Arrange
            string city = "x01";

            //Act
            var entity = await _dataAccesService.RetrieveWeatherDataByCityAsync(city);

            //Assert
            Assert.True(String.IsNullOrWhiteSpace(entity.Name));
        }

    }
}
