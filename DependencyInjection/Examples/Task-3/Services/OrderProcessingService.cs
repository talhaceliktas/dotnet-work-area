using ServiceContracts;
using Microsoft.Extensions.DependencyInjection;

namespace Services
{
    public class OrderProcessingService : IOrderProcessingService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public OrderProcessingService(IServiceScopeFactory serviceScopeFactory) {
            _serviceScopeFactory = serviceScopeFactory;
        }
        public int ProcessPendingOrders()
        {
            using (var scope = _serviceScopeFactory.CreateScope()) {
                var orderRepo = scope.ServiceProvider.GetRequiredService<OrderRepository>();
                return orderRepo.RandomNumber;
            }
        }

    }
}
