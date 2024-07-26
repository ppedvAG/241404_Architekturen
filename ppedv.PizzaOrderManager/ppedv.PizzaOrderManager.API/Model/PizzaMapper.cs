using ppedv.PizzaOrderManager.Model.DomainModel;

namespace ppedv.PizzaOrderManager.API.Model
{
    public static class PizzaMapper
    {
        public static PizzaDTO ToDTO(Pizza pizza)
        {
            return new PizzaDTO
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Price = pizza.Price,
                Size = pizza.Size.ToString(),
                Toppings = pizza.Toppings.Select(t => t.Name).ToArray()
            };
        }

        public static Pizza ToEntity(PizzaDTO pizzaDTO, ICollection<Topping> allToppings)
        {
            return new Pizza
            {
                Id = pizzaDTO.Id,
                Name = pizzaDTO.Name,
                Price = pizzaDTO.Price,
                Size = Enum.Parse<PizzaSize>(pizzaDTO.Size, true),
                Toppings = allToppings.Where(t => pizzaDTO.Toppings.Contains(t.Name)).ToList()
            };
        }
    }
}
