using ppedv.PizzaOrderManager.Model.Contracts;
using ppedv.PizzaOrderManager.Model.DomainModel;

namespace ppedv.PizzaOrderManager.Logic.Tests
{
    class TestRepo : IRepository
    {
        public void Add<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            if (typeof(T) == typeof(Order))
            {
                var result = new List<Order>();
                result.Add(new Order() { DeliveryAddress = new Address() { Id = 1, Name1 = "A1" } });
                result.Add(new Order() { DeliveryAddress = new Address() { Id = 2, Name1 = "A2" } });
                result.Add(new Order() { DeliveryAddress = new Address() { Id = 3, Name1 = "A3" } });
                return result.Cast<T>();
            }

            throw new NotImplementedException();
        }

        public T? GetById<T>(int id) where T : Entity
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }
    }
}