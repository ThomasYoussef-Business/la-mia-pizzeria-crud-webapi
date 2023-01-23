using LaMiaPizzeriaEF.Database;
using LaMiaPizzeriaEF.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LaMiaPizzeriaEF.Controllers {
    [Route("api/pizzas")]
    [ApiController]
    public class PizzasAPIController : ControllerBase {
        [HttpGet]
        public IActionResult Get() {
            using var db = new PizzasDbContext();

            var pizzas = db.Pizzas.Include(p => p.Tags)
                                  .Include(p => p.Category);

            var json = JsonSerializer.Serialize(pizzas);
            return Ok(json);
        }
    }
}
