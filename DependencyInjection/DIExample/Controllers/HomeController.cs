using Autofac;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;

namespace DIExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICitiesService _citiesService1;
        private readonly ICitiesService _citiesService2;
        private readonly ICitiesService _citiesService3;
        //private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILifetimeScope _lifeTimeScope; 

        public HomeController(
            ICitiesService citiesService1,
            ICitiesService citiesService2,
            ICitiesService citiesService3,
            //IServiceScopeFactory scopeFactory
            ILifetimeScope lifeTimeScope
            )
        {
            _citiesService1 = citiesService1;
            _citiesService2 = citiesService2;
            _citiesService3 = citiesService3;
            //_scopeFactory = scopeFactory;
            _lifeTimeScope = lifeTimeScope;
        }

        [Route("/")]
        public IActionResult Index()
        {
            List<string> cities = _citiesService1.GetCities();

            ViewBag.InstanceId_CitiesService_1 = _citiesService1.ServiceInstanceId;
            ViewBag.InstanceId_CitiesService_2 = _citiesService2.ServiceInstanceId;
            ViewBag.InstanceId_CitiesService_3 = _citiesService3.ServiceInstanceId;

            //using (IServiceScope scope = _scopeFactory.CreateScope()) {
            //    ICitiesService citiesService = scope.ServiceProvider.GetRequiredService<ICitiesService>();

            //    ViewBag.InstanceId_CitiesService_4 = citiesService.ServiceInstanceId;
            //}

            using (ILifetimeScope scope = _lifeTimeScope.BeginLifetimeScope())
            {
                ICitiesService citiesService = scope.Resolve<ICitiesService>();

                ViewBag.InstanceId_CitiesService_4 = citiesService.ServiceInstanceId;
            }

            return View(cities);
        }
    }
}
