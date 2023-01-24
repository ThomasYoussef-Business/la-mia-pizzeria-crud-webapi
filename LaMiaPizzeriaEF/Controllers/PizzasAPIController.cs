using LaMiaPizzeriaEF.Database;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LaMiaPizzeriaEF.Controllers {
    [Route("api/pizzas")]
    [ApiController]
    public class PizzasAPIController : ControllerBase {
        [HttpGet]
        public IActionResult Get(string? search) {
            using var db = new PizzasDbContext();

            var pizzas = db.Pizzas.Include(p => p.Tags)
                                  .Include(p => p.Category)
                                  .ToList();


            if (search is not null && !string.IsNullOrWhiteSpace(search)) {
                pizzas = pizzas.Where(p => p.ContainsText(search))
                               .ToList();
            }

            var json = JsonConvert.SerializeObject(pizzas, new JsonSerializerSettings() {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(json);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            using var db = new PizzasDbContext();

            if (id < 1) return BadRequest("Id cannot be less than one.");

            var pizza = db.Pizzas.Find(id);
            if (pizza is not null) {
                db.Entry(pizza)
                  .Reference(p => p.Category)
                  .Load(); // Load category

                db.Entry(pizza)
                  .Collection(p => p.Tags)
                  .Load(); // Load tags

                var json = JsonConvert.SerializeObject(pizza, new JsonSerializerSettings() {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Ok(json);
            }

            return NotFound($"No pizza with ID {id} exists.\nNessuna pizza con ID {id} trovata.");

        }
    }
}
