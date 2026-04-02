using ServiceContracts;

namespace Services
{
    public class ScopedOperation : IScopedOperation
    {
        private readonly Guid _operationId;

        public Guid OperationId => _operationId;

        public ScopedOperation()
        {
            _operationId = Guid.NewGuid();
        }

    }
}
