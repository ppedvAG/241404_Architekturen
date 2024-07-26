using ppedv.PizzaOrderManager.Model.DomainModel;

namespace ppedv.PizzaOrderManager.Logic
{
    public interface IOrderService
    {
        IEnumerable<Order> GetMyOrders(int addressId);
        bool GetOrderStatus(int orderId);
        void PlaceOrder(Order order);
    }
}