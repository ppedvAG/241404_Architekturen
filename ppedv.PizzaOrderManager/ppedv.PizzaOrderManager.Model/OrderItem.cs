namespace ppedv.PizzaOrderManager.Model
{
    public class OrderItem : Entity
    {
        public int Amount { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Pizza? Pizza { get; set; }
    }
}
