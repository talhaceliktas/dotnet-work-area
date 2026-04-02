using ServiceContracts;

namespace Services
{
    public class TransientOperation : ITransientOperation
    {
        private readonly Guid _operationId;

        public Guid OperationId => _operationId;

        public TransientOperation() {
            _operationId = Guid.NewGuid();
        }

    }
}
