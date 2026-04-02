using ServiceContracts;

namespace Services
{
    internal class SingletonOperation : ISingletonOperation
    {
        private readonly Guid _operationId;

        public SingletonOperation()
        {
            _operationId = Guid.NewGuid();
        }

        public Guid GetOperationId()
        {
            return _operationId;
        }
    }
}
