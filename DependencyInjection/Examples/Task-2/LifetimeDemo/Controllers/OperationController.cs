using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace LifetimeDemo.Controllers
{
    public class OperationController : Controller
    {
        private readonly ITransientOperation _transientOperation;
        private readonly IScopedOperation _scopedOperation;
        private readonly ISingletonOperation _singletonOperation;

        public OperationController(
            ITransientOperation transientOperation,
            IScopedOperation scopedOperation,
            ISingletonOperation singletonOperation
        ) {
            _transientOperation = transientOperation;
            _scopedOperation = scopedOperation;
            _singletonOperation = singletonOperation;
        
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            ViewBag.TransientOperation = _transientOperation.GetOperationId();
            ViewBag.ScopedOperation = _scopedOperation.GetOperationId();
            ViewBag.SingletonOperation = _singletonOperation.GetOperationId();

            return View();
        }

    }
}
