using ServiceContracts;

namespace Services
{
    public class OrderProcessingService : IOrderProcessingService
    {
        public OrderProcessingService(IOrderRepository orderRepository) {
        
        }
        public void ProcessPendingOrders()
        {

        }

    }
}
