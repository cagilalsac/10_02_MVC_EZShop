using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BLL.Services
{
    public interface ICartService
    {
        public List<CartItemModel> GetSession(int userId);
        public Service AddToSession(int userId, int productId);
        public Service RemoveFromSession(int userId, int productId);
        public Service ClearSession(int userId);
        public List<CartItemGroupByModel> GetSessionGroupBy(int userId);
    }

    public class CartService : Service, ICartService
    {
        const string SESSIONKEY = "CartKey";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(Db db, IHttpContextAccessor httpContextAccessor) : base(db)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<CartItemModel> GetSession(int userId)
        {
            var cartItems = new List<CartItemModel>();
            var cartItemsJson = _httpContextAccessor.HttpContext.Session.GetString(SESSIONKEY);
            if (!string.IsNullOrWhiteSpace(cartItemsJson))
            {
                cartItems = JsonConvert.DeserializeObject<List<CartItemModel>>(cartItemsJson);
                cartItems = cartItems.Where(ci => ci.UserId == userId).ToList();
            };
            return cartItems;
        }

        private void SetSession(List<CartItemModel> cartItems)
        {
            var cartItemsJson = JsonConvert.SerializeObject(cartItems);
            _httpContextAccessor.HttpContext.Session.SetString(SESSIONKEY, cartItemsJson);
        }

        public Service AddToSession(int userId, int productId)
        {
            var product = _db.Products.Find(productId);
            if (product is null)
                return Error("Product not found!");
            var cartItems = GetSession(userId);
            var cartItem = new CartItemModel()
            {
                Record = product,
                UserId = userId
            };
            cartItems.Add(cartItem);
            SetSession(cartItems);
            return Success($"\"{cartItem.ProductName}\" added to cart successfully.");
        }

        public Service RemoveFromSession(int userId, int productId)
        {
            var cartItems = GetSession(userId);
            var cartItem = cartItems.FirstOrDefault(ci => ci.UserId == userId && ci.ProductId == productId);
            if (cartItem is null)
                return Error("Cart item not found!");
            cartItems.Remove(cartItem);
            SetSession(cartItems);
            return Success("Product removed from cart successfully.");
        }

        public Service ClearSession(int userId)
        {
            var cartItems = GetSession(userId);
            cartItems.RemoveAll(ci => ci.UserId == userId);
            SetSession(cartItems);
            return Success("Cart cleared successfully.");
        }

        public List<CartItemGroupByModel> GetSessionGroupBy(int userId)
        {
            var cartItems = GetSession(userId);
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
            return cartItemsGroupBy;
        }
    }
}
