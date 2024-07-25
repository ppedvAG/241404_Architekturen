namespace ppedv.PizzaOrderManager.Model
{
    public class OrderItem : Entity
    {
        public int Amount { get; set; }
        public Order? Order { get; set; }
        public Pizza? Pizza { get; set; }
    }
}
