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
        [HttpGet]
        public IActionResult GetPizzas(string? str)
        {

            //ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;


            using (PizzeriaContext context = new PizzeriaContext())
            {
                if (str == null)
                {
                    IQueryable<Pizza> pizzas = context.Pizza;
                    return Ok(pizzas.ToList());
                }

                List<Pizza> pizze = context.Pizza.Where(pizze => pizze.Name.Contains(str)).ToList();

                if (pizze.Count <= 0)
                {
                    return NotFound();
                }

                return Ok(pizze);
            }
        }
    }
}
