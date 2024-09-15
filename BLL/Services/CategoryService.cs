using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface ICategoryService
    {
        public IQueryable<CategoryModel> Query();
        public Service Create(Category category);
        public Service Update(Category category);
        public Service Delete(int id);
    }

    public class CategoryService : Service, ICategoryService
    {
        public CategoryService(Db db) : base(db)
        {
        }

        public IQueryable<CategoryModel> Query()
        {
            return _db.Categories.OrderBy(c => c.Name).Select(c => new CategoryModel() { Record = c });
        }

        public Service Create(Category category)
        {
            if (_db.Categories.Any(c => c.Name == category.Name.Trim()))
                return Error("Category with the same name exists!");
            category.Name = category.Name.Trim();
            category.Description = category.Description?.Trim();
            _db.Categories.Add(category);
            _db.SaveChanges();
            return Success("Category created successfully.");
        }

        public Service Update(Category category)
        {
            if (_db.Categories.Any(c => c.Id != category.Id && c.Name == category.Name.Trim()))
                return Error("Category with the same name exists!");
            Category entity = _db.Categories.Find(category.Id);
            entity.Name = category.Name.Trim();
            entity.Description = category.Description?.Trim();
            _db.Categories.Update(entity);
            _db.SaveChanges();
            return Success("Category updated successfully.");
        }

        public Service Delete(int id)
        {
            Category entity = _db.Categories.Include(c => c.Products).FirstOrDefault(c => c.Id == id);
            if (entity.Products.Count > 0)
                return Error("Category has relational products!");
            _db.Categories.Remove(entity);
            _db.SaveChanges();
            return Success("Category deleted successfully.");
        }
    }
}
