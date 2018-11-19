using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrototypeApi;
using PrototypeApi.DbModels;

namespace SPATest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleDataController : Controller
    {
        private readonly ApiContext _context;

        public SampleDataController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public IEnumerable<Product> Products()
        {
            return _context.Products.AsEnumerable();
        }

        [HttpPost]        
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Cannot insert empty value");
            }
            else if (product.Price < 0)
            {
                return BadRequest("Price cannot be less than 0");
            }

            // Check if Product name already exists
            foreach (var prod in _context.Products)
            {
                if (prod.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest("Cannot insert duplicate product");
                }
            }

            // All validations passed, proceed to create the product
            _context.Add(product);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}
