using ppedv.PizzaOrderManager.Model.Contracts;
using ppedv.PizzaOrderManager.Model.DomainModel;

namespace ppedv.PizzaOrderManager.Data.EfCore
{
    public class EfContextIRepositoryAdapter : IRepository
    {
        private EfContext _context;

        public EfContextIRepositoryAdapter(string conString)
        {
            _context = new EfContext(conString);
        }

        public void Add<T>(T entity) where T : Entity
        {
            //if (typeof(T).IsAssignableFrom(typeof(Pizza)))
            //    _context.Pizzas.Add(entity as Pizza);

            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            _context.Remove(entity);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return _context.Set<T>().ToList();
        }

        public T? GetById<T>(int id) where T : Entity
        {
            return _context.Set<T>().Find(id);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Update<T>(T entity) where T : Entity
        {
            _context.Update(entity);
        }
    }
}
