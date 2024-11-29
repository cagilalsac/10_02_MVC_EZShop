using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class StoreModel
    {
        public Store Record { get; set; }

        public string Name => Record.Name;

        [DisplayName("Virtual")]
        public string IsVirtual => Record.IsVirtual ? "Yes" : "No";

        [DisplayName("Product Count")]
        public string ProductCount => Record.ProductStores?.Count.ToString();

        public string Products => string.Join("<br>", Record.ProductStores?.Select(ps => ps.Product?.Name));

        public string Country => Record.Country?.Name;
        public string City => Record.City?.Name;
    }
}
