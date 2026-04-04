using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        public CountriesServiceTest()
        {
            _countriesService = new CountriesService();
        }

        // When CountryAddRequest is null, it should throw ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry()
        {
            // Arrange 
            CountryAddRequest? countryAddRequest = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act
                _countriesService.AddCountry(countryAddRequest);

            });
        }

        // When the CountryName is null, it should throw ArgumentException
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            // Arrange 
            CountryAddRequest? countryAddRequest = new CountryAddRequest() { CountryName = null};

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _countriesService.AddCountry(countryAddRequest);

            });
        }

        // When the CountryName duplicate, it should throw ArgumentException
        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            // Arrange 
            CountryAddRequest? request1 = new CountryAddRequest() { CountryName = "TestCountry" };
            CountryAddRequest? request2 = new CountryAddRequest() { CountryName = "TestCountry" };

            // Assert
            _countriesService.AddCountry(request1);

            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _countriesService.AddCountry(request2);
            });
        }

        // When you supply proper country name, it should insert(add) the country to the existing list of countries
        [Fact]
        public void AddCountry_ProperCountryDetails()
        {
            // Arrange 
            CountryAddRequest? request= new CountryAddRequest() { CountryName = "TestCountry" };

            // Act
            CountryResponse response = _countriesService.AddCountry(request);


            // Assert
            Assert.True(response.CountryId != Guid.Empty);

        }

    }
}
