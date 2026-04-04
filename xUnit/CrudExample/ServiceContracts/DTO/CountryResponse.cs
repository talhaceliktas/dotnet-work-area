using Entities;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class that is used as return type for most of CountriesService methods
    /// </summary>
    public record CountryResponse
    {
        public Guid CountryId { get; init; }

        public string? CountryName { get; init; }
    }

    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse() 
            { CountryId = country.CountryID, CountryName = country.CountryName };
        }
    }
}
