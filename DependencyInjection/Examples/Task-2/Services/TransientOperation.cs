using ServiceContracts;

namespace Services
{
    public class TransientOperation : ITransientOperation
    {
        private readonly Guid _operationId;

        public TransientOperation() {
            _operationId = Guid.NewGuid();
        }

        public Guid GetOperationId()
        {
            return _operationId;
        }
    }
}
