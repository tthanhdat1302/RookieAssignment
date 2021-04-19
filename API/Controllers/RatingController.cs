using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    
    public class RatingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RatingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RatingVm>>> GetRatings()
        {
            return await _context.Ratings
                .Select(x => new RatingVm { RatingId=x.RatingId, RatingScore=x.RatingScore,ProductID=x.ProductID,UserName=x.UserName })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<RatingVm>> GetRating(int id)
        {
            var cate = await _context.Ratings.FindAsync(id);

            if (cate == null)
            {
                return NotFound();
            }

            var cateVm = new RatingVm
            {
                RatingId = cate.RatingId,
                RatingScore = cate.RatingScore,
                ProductID=cate.ProductID,
                UserName=cate.UserName
            };

            return cateVm;
        }


        [HttpPost]
        // [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<ActionResult<RatingVm>> PostRating(RatingVm x)
        {
            var cate = new Rating
            {
                RatingScore=x.RatingScore,
                ProductID=x.ProductID,
                UserName=x.UserName
            };

            _context.Ratings.Add(cate);
            await _context.SaveChangesAsync();
            return Accepted();
        }

        [HttpDelete("{id}")]
        // [Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var cate = await _context.Ratings.FirstOrDefaultAsync(x=>x.RatingId==id);
            if (cate == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(cate);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}