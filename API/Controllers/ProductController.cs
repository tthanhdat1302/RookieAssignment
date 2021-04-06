using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductVm>>> GetProducts()
        {
            return await _context.Products
                .Select(x => new ProductVm { Id = x.Id, Name = x.Name,Price=x.Price,Description=x.Description,Image=x.Image,CategoryId=x.CategoryId })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductVm>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var productVm = new ProductVm
            {
                Id = product.Id,
                Name = product.Name,
                Price=product.Price,
                Description=product.Description,
                Image=product.Image,
                CategoryId=product.CategoryId
            };

            return productVm;
        }

        [HttpPut("{id}")]
        // [Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<IActionResult> PutProduct(int id, ProductCreateRequest productCreateRequest)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = productCreateRequest.Name;
            product.Price=productCreateRequest.Price;
            product.Description=productCreateRequest.Description;
            product.CategoryId=productCreateRequest.CategoryId;
            product.Image=productCreateRequest.Image;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        // [Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductVm>> PostProduct(ProductCreateRequest productCreateRequest)
        {
            var product = new Product
            {
                Name = productCreateRequest.Name,
                Price=productCreateRequest.Price,
                Description=productCreateRequest.Description,
                CategoryId=productCreateRequest.CategoryId,
                Image=productCreateRequest.Image
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, new ProductVm { Id = product.Id, Name = product.Name,Price=product.Price,Description=product.Description,Image=product.Image,CategoryId=product.CategoryId });
        }

        [HttpDelete("{id}")]
        // [Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}