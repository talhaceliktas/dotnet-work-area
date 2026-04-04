using Entities;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class for adding a new country
    /// </summary>
    public record CountryAddRequest
    {
        public string? CountryName { get; init; }

        public Country ToCountry()
        {
            return new Country() { CountryName = CountryName };
        }
    }
}
