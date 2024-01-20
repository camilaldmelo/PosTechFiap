using Domain.Interface.Gateways;

namespace Infra.DB
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly RepositoryBase _session;

        public UnitOfWork(RepositoryBase session)
        {
            _session = session;
        }

        public void BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _session.Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _session.Transaction.Rollback();
            Dispose();
        }

        public void Dispose() => _session.Transaction?.Dispose();
    }
}
