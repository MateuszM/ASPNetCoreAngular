using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfrastructureLayer
{
    public class UnitOfWorkGeneric : IDisposable, IUnitOfWork
    {
        private AppIdentityDBContext db;
        public UnitOfWorkGeneric(AppIdentityDBContext dbs)
        {

            db = dbs;
        }
        // Słownik będzie używany do sprawdzania instancji repozytoriów
        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        public IRepository<T> Repository<T>() where T : class
        {
            // Jeżeli instancja danego repozytorium istnieje - zostanie zwrócona
            if (repositories.Keys.Contains(typeof(T)) == true)
                return repositories[typeof(T)] as IRepository<T>;
            // Jeżeli nie, zostanie utworzona nowa i dodana do słownika
            IRepository<T> repo = new GenericRepository<T>(db);
            repositories.Add(typeof(T), repo);
            return repo;
        }


        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    db.Dispose();
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> Commit()
        {
            return await db.SaveChangesAsync();
        }

        public void Rollback()
        {
            db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
    }
}