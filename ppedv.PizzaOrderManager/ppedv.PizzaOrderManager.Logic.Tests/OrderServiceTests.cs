using Moq;
using ppedv.PizzaOrderManager.Model.Contracts;
using ppedv.PizzaOrderManager.Model.DomainModel;

namespace ppedv.PizzaOrderManager.Logic.Tests
{
    public class OrderServiceTests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void GetMyOrders_throws_ArgumentEx(int id)
        {
            var os = new OrderService(null);

            Assert.Throws<ArgumentException>(() => os.GetMyOrders(id));
        }

        [Fact]
        public void GetMyOrders_if_my_id_was_a_delivery_address_it_should_be_in_the_result()
        {
            var os = new OrderService(new TestRepo());

            var result = os.GetMyOrders(2);

            Assert.Equal("A2", result.First().DeliveryAddress.Name1);
        }


        [Fact]
        public void GetMyOrders_if_my_id_was_a_delivery_address_it_should_be_in_the_result_moq()
        {
            var repoMock = new Mock<IRepository>();
            repoMock.Setup(x => x.GetAll<Order>()).Returns(() =>
            {
                var result = new List<Order>
                {
                    new Order() { DeliveryAddress = new Address() { Id = 1, Name1 = "A1" } },
                    new Order() { DeliveryAddress = new Address() { Id = 2, Name1 = "A2" } },
                    new Order() { DeliveryAddress = new Address() { Id = 3, Name1 = "A3" } }
                };
                return result;
            });
            var os = new OrderService(repoMock.Object);

            var result = os.GetMyOrders(2);

            Assert.Equal("A2", result.First().DeliveryAddress.Name1);
        }
    }

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