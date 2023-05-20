using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaCategoriesController : ControllerBase
    {
        private readonly PizzeriaContext _context;

        public PizzaCategoriesController(PizzeriaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            List<PizzaCategory> categories = _context.PizzaCategories.ToList<PizzaCategory>();

            return Ok(categories);
        }
    }
}
