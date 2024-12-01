using BLL.Controllers.Bases;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    [Authorize]
    public class CartController : MvcController
    {
        const string SESSIONKEY = "cart";

        private readonly HttpServiceBase _httpService;
        private readonly IService<Product, ProductModel> _productService;

        public CartController(HttpServiceBase httpService, IService<Product, ProductModel> productService)
        {
            _httpService = httpService;
            _productService = productService;
        }

        private List<CartItemModel> GetSession(int userId)
        {
            var cart = _httpService.GetSession<List<CartItemModel>>(SESSIONKEY) ?? new List<CartItemModel>();
            return cart.OrderBy(c => c.ProductName).Where(c => c.UserId == userId).ToList();
        }

        private int GetUserId() => Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == "Id").Value);

        // GET: Cart
        public IActionResult Index()
        {
            var cart = GetSession(GetUserId());
            return View(cart);
        }

        // GET: Cart/Add/7
        public IActionResult Add(int productId)
        {
            var userId = GetUserId();
            var cart = GetSession(userId);
            var product = _productService.Query().SingleOrDefault(q => q.Record.Id == productId);
            if (product is null)
            {
                TempData["Message"] = "Product not found!";
            }
            else
            {
                var cartItem = new CartItemModel()
                {
                    ProductId = product.Record.Id,
                    ProductName = product.Record.Name,
                    ProductUnitPrice = product.Record.UnitPrice,
                    UserId = userId
                };
                cart.Add(cartItem);
                _httpService.SetSession(SESSIONKEY, cart);
                TempData["Message"] = $"\"{cartItem.ProductName}\" added to cart successfully.";
            }
            return RedirectToAction("Index", "Products");
        }

        // GET: Cart/Remove/7
        public IActionResult Remove(int productId)
        {
            var cart = GetSession(GetUserId());
            var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);
            cart.Remove(cartItem);
            _httpService.SetSession(SESSIONKEY, cart);
            TempData["Message"] = "Product removed from cart successfully.";
            return RedirectToAction(nameof(IndexGroupBy));
        }

        // GET: Cart/Clear
        public IActionResult Clear()
        {
            var userId = GetUserId();
            var cart = GetSession(userId);
            cart.RemoveAll(c => c.UserId == userId);
            _httpService.SetSession(SESSIONKEY, cart);
            TempData["Message"] = "Cart cleared successfully.";
            return RedirectToAction(nameof(IndexGroupBy));
        }

        // GET: Cart/IndexGroupBy
        public IActionResult IndexGroupBy()
        {
            var cartItems = GetSession(GetUserId());
            var cartItemsGroupBy = (from ci in cartItems
                                    group ci by new { ci.UserId, ci.ProductId, ci.ProductName }
                                    into ciGroupBy
                                    select new CartItemGroupByModel()
                                    {
                                        ProductName = ciGroupBy.Key.ProductName,
                                        ProductId = ciGroupBy.Key.ProductId,
                                        UserId = ciGroupBy.Key.UserId,
                                        ProductUnitPrice = ciGroupBy.Sum(cig => cig.ProductUnitPrice).ToString("C2"),
                                        ProductCount = ciGroupBy.Count()
                                    }).ToList();
            cartItemsGroupBy.Add(new CartItemGroupByModel()
            {
                IsTotal = true,
                ProductName = "Total:",
                TotalProductCount = cartItemsGroupBy.Sum(cig => cig.ProductCount),
                TotalProductUnitPrice = cartItems.Sum(cig => cig.ProductUnitPrice).ToString("C2")
            });
            return View(cartItemsGroupBy);
        }
    }
}
