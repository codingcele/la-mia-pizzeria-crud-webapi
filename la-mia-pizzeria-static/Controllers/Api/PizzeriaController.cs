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
    [Route("api/[controller]")]
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
            List<Pizza> pizzas;
            pizzas = _context.Pizza.Include(p => p.Ingredients).Include(p => p.PizzaCategory).ToList<Pizza>();
            
            if (str != null)
            {
                pizzas = pizzas.Where(pizza => pizza.Name.ToLower().Contains(str.ToLower())).ToList();
            }

            return Ok(pizzas);
        }

        [HttpGet("{id}")]
        public IActionResult GetPizzaById(int id)
        {
            Pizza? pizza = _context.Pizza.Include(p => p.Ingredients).Include(p => p.PizzaCategory).FirstOrDefault(p => p.Id == id);

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
                
                return Ok("Pizza eliminata correttamente");
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

            if (data.Ingredients != null)
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

        [HttpPut("{id}")]
        public IActionResult ModificaPizza(int id, [FromBody] Pizza data)
        {
            Pizza? pizzaToEdit = _context.Pizza.Include(p => p.Ingredients).FirstOrDefault(p => p.Id == id);
            if (pizzaToEdit == null)
                return NotFound($"La pizza con id {id} non esiste!");
            else
            {
                pizzaToEdit.Image = data.Image;
                pizzaToEdit.Name = data.Name;
                pizzaToEdit.Description = data.Description;
                pizzaToEdit.Price = data.Price;
                pizzaToEdit.Ingredients.Clear();

                pizzaToEdit.PizzaCategoryId = data.PizzaCategoryId;

                pizzaToEdit.Ingredients = new List<Ingredient>();

                if (data != null && data.Ingredients != null)
                {
                    foreach (Ingredient ingredient in data.Ingredients)
                    {
                        Ingredient? ing = _context.Ingredients
                                    .Where(x => x.Id == ingredient.Id)
                                    .FirstOrDefault();
                        pizzaToEdit.Ingredients.Add(ing);
                    }
                }

                _context.SaveChanges();

                return Ok();
            }
        }
    }
}