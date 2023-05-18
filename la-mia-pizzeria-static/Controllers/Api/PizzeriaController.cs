using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using NuGet.Packaging.Signing;
using System.Net;
using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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

        [HttpGet("{id}")]
        public IActionResult GetPizzaById(int id)
        {
            Pizza pizza = _context.Pizza.FirstOrDefault(p => p.Id == id);

            if (pizza == null)
            {
                return NotFound();
            }

            return Ok(pizza);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Pizza? pizzaToDelete = _context.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();
                
            if (pizzaToDelete != null)
            {
                _context.Pizza.Remove(pizzaToDelete);
                _context.SaveChanges();
                
                return Ok("Post eliminato correttamente");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CreaPizza([FromBody] Pizza data)
        {
            Pizza pizzaToCreate = new Pizza();
            pizzaToCreate.Image = data.Image;
            pizzaToCreate.Name = data.Name;
            pizzaToCreate.Description = data.Description;
            pizzaToCreate.Price = data.Price;

            pizzaToCreate.PizzaCategoryId = data.PizzaCategoryId;



            pizzaToCreate.Ingredients = new List<Ingredient>();



            if (data != null && data.Ingredients != null)
            {
                foreach (Ingredient ingredient in data.Ingredients)
                {
                    Ingredient? ing = _context.Ingredients
                                .Where(x => x.Id == ingredient.Id)
                                .FirstOrDefault();
                    pizzaToCreate.Ingredients.Add(ing);
                }
            }

            _context.Pizza.Add(data);

            _context.SaveChanges();

            return Ok();
        }
    }
}