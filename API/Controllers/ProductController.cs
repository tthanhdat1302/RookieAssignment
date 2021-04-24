using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductVm>>> GetProducts()
        {
            return await _context.Products
                .Select(x => new ProductVm { Id = x.Id, Name = x.Name, Price = x.Price, Description = x.Description, Image = x.Image, RatingAVG = x.RatingAVG, CategoryId = x.CategoryId })
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
                Price = product.Price,
                Description = product.Description,
                Image = product.Image,
                RatingAVG = product.RatingAVG,
                CategoryId = product.CategoryId
            };

            return productVm;
        }
        [HttpPut]
        [Route("rating/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> PutRatingAvg(int id, ProductCreateRequest productCreateRequest)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            if (productCreateRequest.ImageFile != null)
            {
                productCreateRequest.Image = await SaveImage(productCreateRequest.ImageFile);
            }

            product.Name = productCreateRequest.Name;
            product.Price = productCreateRequest.Price;
            product.Description = productCreateRequest.Description;
            product.CategoryId = productCreateRequest.CategoryId;
            product.Image = productCreateRequest.Image;
            product.RatingAVG = productCreateRequest.RatingAVG;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        // [Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<IActionResult> PutProduct(int id, [FromForm] ProductCreateRequest productCreateRequest)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            if (productCreateRequest.ImageFile != null)
            {
                productCreateRequest.Image = await SaveImage(productCreateRequest.ImageFile);
            }

            product.Name = productCreateRequest.Name;
            product.Price = productCreateRequest.Price;
            product.Description = productCreateRequest.Description;
            product.CategoryId = productCreateRequest.CategoryId;
            product.Image = productCreateRequest.Image;
            product.RatingAVG = productCreateRequest.RatingAVG;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostProduct([FromForm] ProductCreateRequest productCreateRequest)
        {
            if(productCreateRequest.ImageFile!=null)
            {
                productCreateRequest.Image = await SaveImage(productCreateRequest.ImageFile);
            }
            var product = new Product
            {
                Name = productCreateRequest.Name,
                Price = productCreateRequest.Price,
                Description = productCreateRequest.Description,
                Image = productCreateRequest.Image,
                RatingAVG = productCreateRequest.RatingAVG,
                CategoryId = productCreateRequest.CategoryId
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }
        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "image", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
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