using BLL.Controllers.Bases;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MVC.Controllers
{
    [Authorize(Roles = "User")]
    public class CartController : MvcController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        private int GetUserId() => Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);

        // GET: Cart
        public IActionResult Index()
        {
            var list = _cartService.GetSession(GetUserId());
            return View(list);
        }

        // GET: Cart/Add/7
        public IActionResult Add(int productId)
        {
            var result = _cartService.AddToSession(GetUserId(), productId);
            TempData["Message"] = result.Message;
            return RedirectToAction("Index", "Products");
        }

        // GET: Cart/Remove/7
        public IActionResult Remove(int productId)
        {
            var result = _cartService.RemoveFromSession(GetUserId(), productId);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(IndexGroupBy));
        }

        // GET: Cart/Clear
        public IActionResult Clear()
        {
            var result = _cartService.ClearSession(GetUserId());
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(IndexGroupBy));
        }

        // GET: Cart/IndexGroupBy
        public IActionResult IndexGroupBy()
        {
            var list = _cartService.GetSessionGroupBy(GetUserId());
            return View(list);
        }
    }
}
