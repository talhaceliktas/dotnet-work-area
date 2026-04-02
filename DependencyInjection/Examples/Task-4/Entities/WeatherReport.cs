namespace Entities
{
    public class WeatherReport
    {
        public Guid Id { get; set; }

        public string? City { get; set; }

        public double TemperatureCelsius { get; set; }

        public string? Description { get; set; }

        public DateTime FetchedAt { get; set; }
    }
}
