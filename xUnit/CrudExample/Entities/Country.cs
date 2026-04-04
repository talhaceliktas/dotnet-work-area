namespace Entities
{
    /// <summary>
    /// Domain model for Country
    /// </summary>
    public class ICountriesService
    {
        public Guid CountryID { get; set; }

        public string? CountryName { get; set; }
    }
}
