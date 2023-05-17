using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using NuGet.Packaging.Signing;
using System.Net;

namespace la_mia_pizzeria_static.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzeriaController : ControllerBase
    {
        private readonly PizzeriaContext _context;

        public PizzeriaController(PizzeriaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetPizzas(string? str)
        {
            if (str == null)
            {
                IQueryable<Pizza> pizzas = _context.Pizza;
                return Ok(pizzas.ToList());
            }

            List<Pizza> pizze = _context.Pizza.Where(pizze => pizze.Name.Contains(str)).ToList();

            if (pizze.Count <= 0)
            {
                return NotFound();
            }

            return Ok(pizze);
        }

        [HttpGet]
        public IActionResult GetPizzaById(int id)
        {
            Pizza pizza = _context.Pizza.FirstOrDefault(p => p.Id == id);

            if (pizza == null)
            {
                return NotFound();
            }

            return Ok(pizza);
        }
    }
}