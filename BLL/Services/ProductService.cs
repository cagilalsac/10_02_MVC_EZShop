using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IProductService
    {
        public IQueryable<ProductModel> Query();
        public Service Create(Product product);
        public Service Update(Product product);
        public Service Delete(int id);

        public List<ProductModel> GetList(PageModel pageModel);
    }

    public class ProductService : Service, IProductService
    {
        public ProductService(Db db) : base(db)
        {
        }

        public IQueryable<ProductModel> Query()
        {
            return _db.Products.Include(p => p.Category).Include(p => p.ProductStores).ThenInclude(ps => ps.Store)
                .OrderBy(p => p.StockAmount).ThenByDescending(p => p.UnitPrice).ThenBy(p => p.Name).Select(p => new ProductModel() { Record = p });
        }

        public Service Create(Product product)
        {
            if ((product.StockAmount ?? 0) < 0)
                return Error("Stock amount must be 0 or a positive number!");
            if (product.UnitPrice <= 0 || product.UnitPrice > 100000)
                return Error("Unit price must be greater than 0 and less than 100000!");
            if (_db.Products.Any(p => p.Name.ToUpper() == product.Name.ToUpper().Trim()))
                return Error("Product with the same name exists!");
            product.Name = product.Name.Trim();
            _db.Products.Add(product);
            _db.SaveChanges();
            return Success("Product created successfully.");
        }

        public Service Update(Product product)
        {
            if ((product.StockAmount ?? 0) < 0)
                return Error("Stock amount must be 0 or a positive number!");
            if (product.UnitPrice <= 0 || product.UnitPrice > 100000)
                return Error("Unit price must be greater than 0 and less than 100000!");
            if (_db.Products.Any(p => p.Id != product.Id && p.Name.ToUpper() == product.Name.ToUpper().Trim()))
                return Error("Product with the same name exists!");
            var entity = _db.Products.Include(p => p.ProductStores).SingleOrDefault(p => p.Id == product.Id);
            if (entity is null)
                return Error("Product not found!");
            _db.ProductStores.RemoveRange(entity.ProductStores);
            entity.Name = product.Name.Trim();
            entity.UnitPrice = product.UnitPrice;
            entity.StockAmount = product.StockAmount;
            entity.ExpirationDate = product.ExpirationDate;
            entity.CategoryId = product.CategoryId;
            entity.ProductStores = product.ProductStores;
            _db.Products.Update(entity);
            _db.SaveChanges();
            return Success("Product updated successfully.");
        }

        public Service Delete(int id)
        {
            var entity = _db.Products.Include(p => p.ProductStores).SingleOrDefault(p => p.Id == id);
            if (entity is null)
                return Error("Product not found!");
            _db.ProductStores.RemoveRange(entity.ProductStores);
            _db.Products.Remove(entity);
            _db.SaveChanges();
            return Success("Product deleted successfully.");
        }

        public List<ProductModel> GetList(PageModel pageModel)
        {
            var query = Query();
            pageModel.TotalRecordsCount = query.Count();
            int recordsPerPageCount;
            if (int.TryParse(pageModel.RecordsPerPageCount, out recordsPerPageCount))
                query = query.Skip((pageModel.PageNumber - 1) * recordsPerPageCount).Take(recordsPerPageCount);
            return query.ToList();
        }
    }
}
