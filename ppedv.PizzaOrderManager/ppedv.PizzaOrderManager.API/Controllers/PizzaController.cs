using Microsoft.AspNetCore.Mvc;
using ppedv.PizzaOrderManager.API.Model;
using ppedv.PizzaOrderManager.Model.Contracts;
using ppedv.PizzaOrderManager.Model.DomainModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ppedv.PizzaOrderManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IRepository repo;

        public PizzaController(IRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/<PizzaController>
        [HttpGet]
        public IEnumerable<PizzaDTO> Get()
        {
            return repo.GetAll<Pizza>().Select(x => PizzaMapper.ToDTO(x));
        }

        // GET api/<PizzaController>/5
        [HttpGet("{id}")]
        public ActionResult<PizzaDTO> Get(int id)
        {
            var pizza = repo.GetById<Pizza>(id);
            if (pizza == null)
            {
                return NotFound();
            }
            return Ok(PizzaMapper.ToDTO(pizza));
        }

        // POST api/<PizzaController>
        [HttpPost]
        public void Post([FromBody] PizzaDTO newPizza)
        {
            repo.Add(PizzaMapper.ToEntity(newPizza, repo.GetAll<Topping>().ToList()));
            repo.SaveChanges();
        }

        // PUT api/<PizzaController>/
        [HttpPut()]
        public void Put([FromBody] PizzaDTO pizza)
        {
            repo.Update(PizzaMapper.ToEntity(pizza, repo.GetAll<Topping>().ToList()));
            repo.SaveChanges();
        }

        // DELETE api/<PizzaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var toKill = repo.GetById<Pizza>(id);
            if (toKill == null)
                return NotFound();

            repo.Delete(toKill);
            repo.SaveChanges();

            return Ok();
        }
    }
}
