namespace ppedv.PizzaOrderManager.Model.DomainModel
{
    public class Topping : Entity
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public virtual ICollection<Pizza> Pizzas { get; set; } = new HashSet<Pizza>();
    }
}
