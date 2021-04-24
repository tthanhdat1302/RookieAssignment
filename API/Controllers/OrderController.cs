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

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<OrderVm>>> GetOrders()
        {
            return await _context.Orders
                .Select(x => new OrderVm { OrderId = x.OrderId, OrderQuantity = x.OrderQuantity,OrderTotal=x.OrderTotal,UserName=x.UserName })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<OrderVm>> GetOrderById(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderVm = new OrderVm
            {
                OrderId = order.OrderId,
                OrderQuantity = order.OrderQuantity,
                OrderTotal=order.OrderTotal,
                UserName=order.UserName
               
            };

            return orderVm;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostOrder(OrderCreateRequest orderCreateRequest)
        {
      
            var order = new Order
            {
                OrderQuantity=orderCreateRequest.OrderQuantity,
                OrderTotal=orderCreateRequest.OrderTotal,
                UserName=orderCreateRequest.UserName
            };     
            // product.ImageFile=productCreateRequest.ImageFile;
            // product.Image=await SaveImage(product.ImageFile);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}