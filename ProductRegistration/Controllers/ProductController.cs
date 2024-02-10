using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductRegistration.API.DataContext;
using ProductRegistration.Domain.Models;
using System;
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
        public async Task<ActionResult<Product>> Get(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            return Ok(product);
        }

        [HttpPost]
        [Route("CreateRecord")]
        public async Task<ActionResult<Product>> POST(Product product)
        {
            if (product == null)
                return BadRequest();
            
            this._context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<ActionResult<Product>> Update(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var productData = await _context.Products.FindAsync(id);
            if (productData == null)            
                return NotFound();
            
            if(!ModelState.IsValid)
                return BadRequest();

            _context.Entry(productData).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<ActionResult<Product>> Delete(Guid id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok();            
        }
    }
}