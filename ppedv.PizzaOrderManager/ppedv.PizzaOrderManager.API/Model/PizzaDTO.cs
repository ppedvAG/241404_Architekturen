namespace ppedv.PizzaOrderManager.API.Model
{
    public class PizzaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Size { get; set; } = string.Empty;

        public string[] Toppings { get; set; } = [];

    }

}
