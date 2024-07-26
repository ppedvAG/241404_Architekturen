﻿using ppedv.PizzaOrderManager.Model.Contracts;
using ppedv.PizzaOrderManager.Model.DomainModel;

namespace ppedv.PizzaOrderManager.Logic
{
    public class OrderService
    {
        private IRepository repository;

        public OrderService(IRepository repository)
        {
            this.repository = repository;
        }

        public void PlaceOrder(Order order)
        {
            throw new NotImplementedException();
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
