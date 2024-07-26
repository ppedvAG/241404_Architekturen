using ppedv.PizzaOrderManager.Model.Contracts;
using ppedv.PizzaOrderManager.Model.DomainModel;

namespace ppedv.PizzaOrderManager.Logic
{
    public class OrderService : IOrderService
    {
        private readonly IRepository repository;
        private readonly IPizzaService pizzaService;

        public OrderService(IRepository repository, IPizzaService pizzaService)
        {
            this.repository = repository;
            this.pizzaService = pizzaService;
        }

        public void PlaceOrder(Order order)
        {
            foreach (var item in order.Items)
            {
                if (item.Pizza == null)
                    throw new ArgumentNullException("Die Pizza darf nicht null sein");

                if (!pizzaService.IsPizzaAvailable(item.Pizza.Id))
                    throw new InvalidProgramException($"Die Pizza {item.Pizza.Name} mit der ID {item.Pizza.Id} ist nicht verfügbar");
            }

            repository.Add(order);
            repository.SaveChanges();
        }

        public IEnumerable<Order> GetMyOrders(int addressId)
        {
            if (addressId <= 0)
                throw new ArgumentException("Id kann ich nicht kleiner als 1 sein");

            return repository.GetAll<Order>()
                             .Where(x => x.DeliveryAddress?.Id == addressId ||
                                         x.BillingAddress?.Id == addressId);
        }

        public bool GetOrderStatus(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
