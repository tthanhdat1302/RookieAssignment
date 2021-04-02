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
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CategoryVm>>> GetCategories()
        {
            return await _context.Categories
                .Select(x => new CategoryVm { Id = x.Id, Name = x.Name })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CategoryVm>> GetCategory(int id)
        {
            var cate = await _context.Categories.FindAsync(id);

            if (cate == null)
            {
                return NotFound();
            }

            var cateVm = new CategoryVm
            {
                Id = cate.Id,
                Name = cate.Name
            };

            return cateVm;
        }

        [HttpPut("{id}")]
        // [Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<IActionResult> PutCategory(int id, CategoryCreateRequest categoryCreateRequest)
        {
            var cate = await _context.Categories.FindAsync(id);

            if (cate == null)
            {
                return NotFound();
            }

            cate.Name = categoryCreateRequest.Name;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        // [AllowAnonymous]
        public async Task<ActionResult<CategoryVm>> PostCategory(CategoryCreateRequest categoryCreateRequest)
        {
            var cate = new Category
            {
                Name = categoryCreateRequest.Name
            };

            _context.Categories.Add(cate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = cate.Id }, new CategoryVm { Id = cate.Id, Name = cate.Name });
        }

        [HttpDelete("{id}")]
        // [Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var cate = await _context.Categories.FindAsync(id);
            if (cate == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(cate);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}