using ppedv.PizzaOrderManager.Model.Contracts;
using ppedv.PizzaOrderManager.Model.DomainModel;

namespace ppedv.PizzaOrderManager.Logic
{

    public class PizzaService : IPizzaService
    {
        private readonly IRepository repo;

        public PizzaService(IRepository repo)
        {
            this.repo = repo;
        }
        public bool IsPizzaAvailable(int id)
        {
            return repo.GetById<Pizza>(id) != null;
        }
    }
}
