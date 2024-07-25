﻿namespace ppedv.PizzaOrderManager.Model.DomainModel
{
    public class Order : Entity
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public virtual Address? DeliveryAddress { get; set; }
        public virtual Address? BillingAddress { get; set; }
    }
}