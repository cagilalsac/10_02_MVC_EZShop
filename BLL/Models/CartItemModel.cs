using System.ComponentModel;

namespace BLL.Models
{
    public class CartItemModel
    {
        [DisplayName("Product")]
        public string ProductName { get; set; }

        [DisplayName("Product Unit Price")]
        public decimal ProductUnitPrice { get; set; }

        public int ProductId { get; set; }
        public int UserId { get; set; }
    }

    public class CartItemGroupByModel
    {
        [DisplayName("Product")]
        public string ProductName { get; set; }

        [DisplayName("Product Unit Price")]
        public string ProductUnitPrice { get; set; }

        [DisplayName("Product Count")]
        public int ProductCount { get; set; }

        [DisplayName("Total Product Unit Price")]
        public string TotalProductUnitPrice { get; set; }

        [DisplayName("Total Product Count")]
        public int TotalProductCount { get; set; }

        public bool IsTotal { get; set; }

        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
