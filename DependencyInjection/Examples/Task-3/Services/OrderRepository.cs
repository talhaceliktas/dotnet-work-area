using ServiceContracts;

namespace Services
{
    public class OrderRepository : IOrderRepository
    {
        public int RandomNumber { get
            {
                Random random = new Random();
                return random.Next(0, 100);
            } }
    }
}
