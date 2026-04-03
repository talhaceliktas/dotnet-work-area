using ServiceContracts;

namespace Services
{
    public class SingletonOperation : ISingletonOperation
    {
        private readonly Guid _operationId;

        public Guid OperationId => _operationId;

        public SingletonOperation()
        {
            _operationId = Guid.NewGuid();
        }
    }
}
