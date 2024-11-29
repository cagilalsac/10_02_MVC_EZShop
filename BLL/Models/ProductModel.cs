﻿using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class ProductModel
    {
        public Product Record { get; set; }

        public string Name => Record.Name;

        [DisplayName("Unit Price")]
        public string UnitPrice => Record.UnitPrice.ToString("C2");

        [DisplayName("Stock Amount")]
        public string StockAmount => Record.StockAmount.HasValue ?
            ("<span class=" +
                (Record.StockAmount.Value < 10 ? "\"badge bg-danger\">"
                : Record.StockAmount.Value < 100 ? "\"badge bg-warning\">"
                : "\"badge bg-success\">") + Record.StockAmount.Value + "</span>")
            : string.Empty;

        [DisplayName("Expiration Date")]
        public string ExpirationDate => Record.ExpirationDate.HasValue ? Record.ExpirationDate.Value.ToShortDateString() : "";

        public string Category => Record.Category?.Name;

        public string Stores => string.Join("<br>", Record.ProductStores?.OrderBy(ps => ps.Store?.Name).Select(ps => ps.Store?.Name));

        [DisplayName("Stores")]
        public List<int> StoreIds 
        { 
            get => Record.ProductStores?.Select(ps => ps.StoreId).ToList();
            set => Record.ProductStores = value.Select(v => new ProductStore() { StoreId = v }).ToList();
        }
    }
}
