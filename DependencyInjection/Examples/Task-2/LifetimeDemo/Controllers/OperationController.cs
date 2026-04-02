using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace LifetimeDemo.Controllers
{
    public class OperationController : Controller
    {
        private readonly ITransientOperation _transient1;
        private readonly ITransientOperation _transient2;
        private readonly IScopedOperation _scoped1;
        private readonly IScopedOperation _scoped2;
        private readonly ISingletonOperation _singleton1;
        private readonly ISingletonOperation _singleton2;

        public OperationController(
            ITransientOperation transientOperation1,
            ITransientOperation transientOperation2,
            IScopedOperation scopedOperation1,
            IScopedOperation scopedOperation2,
            ISingletonOperation singletonOperation1,
            ISingletonOperation singletonOperation2
        ) {
            _transient1 = transientOperation1;
            _scoped1 = scopedOperation1;
            _singleton1 = singletonOperation1;

            _transient2 = transientOperation2;
            _scoped2 = scopedOperation2;
            _singleton2 = singletonOperation2;

        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            ViewBag.TransientOperation1 = _transient1.OperationId;
            ViewBag.ScopedOperation1 = _scoped1.OperationId;
            ViewBag.SingletonOperation1 = _singleton1.OperationId;
            ViewBag.TransientOperation2 = _transient2.OperationId;
            ViewBag.ScopedOperation2 = _scoped2.OperationId;
            ViewBag.SingletonOperation2 = _singleton2.OperationId;

            return View();
        }

    }
}
