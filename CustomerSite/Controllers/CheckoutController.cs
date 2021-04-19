using Microsoft.AspNetCore.Mvc;
using CustomerSite.Helpers;
using CustomerSite.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using CustomerSite.Services;
using Shared;
using System.Security.Claims;

namespace CustomerSite.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IOrderClient _orderClient;
        public CheckoutController(IOrderClient orderClient)
        {
            _orderClient = orderClient;
        }
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(items => items.Product.Price * items.Quantity);
            return View();
        }
        public IActionResult Pay()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(items => items.Product.Price * items.Quantity);
            int count = 0;
            foreach (var item in cart)
            {
                count++;
            }
            OrderCreateRequest orderCreateRequest = new OrderCreateRequest
            {
                OrderQuantity = count,
                OrderTotal = ViewBag.total,
                UserName = User.Identity.Name,
            };
            _orderClient.PostOrder(orderCreateRequest);
            cart.Clear();
            ViewBag.cart = cart;
            ViewBag.total = 0;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index", "Home");
        }
    }
}