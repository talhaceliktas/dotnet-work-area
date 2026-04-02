using ServiceContracts;

namespace Services
{
    public class ScopedOperation : IScopedOperation
    {
        private readonly Guid _operationId;

        public ScopedOperation()
        {
            _operationId = Guid.NewGuid();
        }

        public Guid GetOperationId()
        {
            return _operationId;
        }
    }
}
