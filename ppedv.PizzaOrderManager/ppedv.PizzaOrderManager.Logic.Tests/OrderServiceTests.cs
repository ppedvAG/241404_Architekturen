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
            var os = new OrderService(null,null);

            Assert.Throws<ArgumentException>(() => os.GetMyOrders(id));
        }

        [Fact]
        public void GetMyOrders_if_my_id_was_a_delivery_address_it_should_be_in_the_result()
        {
            var os = new OrderService(new TestRepo(),null);

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
            var os = new OrderService(repoMock.Object,null);

            var result = os.GetMyOrders(2);

            Assert.Equal("A2", result.First().DeliveryAddress.Name1);
        }




        [Fact]
        public void PlaceOrder_ThrowsArgumentNullException_WhenPizzaIsNull()
        {
            var os = new OrderService(null, null);

            // Arrange
            var order = new Order
            {
                Items = new List<OrderItem>
            {
                new OrderItem { Pizza = null }
            }
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => os.PlaceOrder(order));
            Assert.Equal("Die Pizza darf nicht null sein", exception.ParamName);
        }

        [Fact]
        public void PlaceOrder_ThrowsInvalidProgramException_WhenPizzaIsNotAvailable()
        {
            var pizzaServiceMock = new Mock<IPizzaService>();
            var os = new OrderService(null, pizzaServiceMock.Object);

            // Arrange
            var pizza = new Pizza { Id = 1, Name = "Margherita" };
            var order = new Order
            {
                Items = new List<OrderItem>
            {
                new OrderItem { Pizza = pizza }
            }
            };

            pizzaServiceMock.Setup(ps => ps.IsPizzaAvailable(pizza.Id)).Returns(false);

            // Act & Assert
            var exception = Assert.Throws<InvalidProgramException>(() => os.PlaceOrder(order));
            Assert.Equal($"Die Pizza {pizza.Name} mit der ID {pizza.Id} ist nicht verfügbar", exception.Message);
        }

        [Fact]
        public void PlaceOrder_AddsOrder_WhenPizzaIsAvailable()
        {
            var repoMock = new Mock<IRepository>();
            var pizzaServiceMock = new Mock<IPizzaService>();
            var os = new OrderService(repoMock.Object,pizzaServiceMock.Object);
            // Arrange
            var pizza = new Pizza { Id = 1, Name = "Margherita" };
            var order = new Order
            {
                Items = new List<OrderItem>
            {
                new OrderItem { Pizza = pizza }
            }
            };

            pizzaServiceMock.Setup(ps => ps.IsPizzaAvailable(pizza.Id)).Returns(true);

            // Act
            os.PlaceOrder(order);

            // Assert
            repoMock.Verify(r => r.Add(order), Times.Once);
            repoMock.Verify(r => r.SaveChanges(), Times.Exactly(1));
        }
    }
}