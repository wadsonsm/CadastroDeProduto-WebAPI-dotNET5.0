using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductRegistration.API.DataContext;
using ProductRegistration.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductRegistration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MyDataContext _context;
        public ProductController(MyDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet]
        [Route("ProductDetails")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var product = await _context.Products.FindAsync(id);
            return Ok(product);
        }
    }
}
