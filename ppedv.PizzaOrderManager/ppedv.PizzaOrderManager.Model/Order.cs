namespace ppedv.PizzaOrderManager.Model
{
    public class Order : Entity
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public Address? DeliveryAddress { get; set; }
        public Address? BillingAddress { get; set; }
    }
}
