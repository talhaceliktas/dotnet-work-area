namespace Services
{
    public class CitiesService
    {
        private List<string> _cities;

        public CitiesService()
        {
            _cities = new List<string>()
            {
                "London",
                "Berlin",
                "Mardin",
                "Lisbon",
                "Istanbul",
                "Tokyo"
            };   
        }

        public List<string> GetCities()
        {
            return _cities;
        }
    }
}
