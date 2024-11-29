using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class StoreService : Service, IService<Store, StoreModel>
    {
        protected override string OperationFailed => "Invalid operation!";

        public StoreService(Db db) : base(db)
        {
        }

        public IQueryable<StoreModel> Query()
        {
            return _db.Stores.Include(s => s.ProductStores).ThenInclude(ps => ps.Product).Include(s => s.Country).Include(s => s.City)
                .OrderByDescending(s => s.IsVirtual).ThenBy(s => s.Name).Select(s => new StoreModel() { Record = s });
        }

        public Service Create(Store store)
        {
            if (_db.Stores.Any(s => s.Name.ToUpper() == store.Name.ToUpper().Trim() && s.IsVirtual == store.IsVirtual))
                return Error("Store with the same name exists!");
            store.Name = store.Name.Trim();
            _db.Add(store);
            _db.SaveChanges();
            return Success("Store created successfully.");
        }

        public Service Update(Store store)
        {
            if (_db.Stores.Any(s => s.Id != store.Id && s.Name.ToUpper() == store.Name.ToUpper().Trim() && s.IsVirtual == store.IsVirtual))
                return Error("Store with the same name exists!");
            Store entity = _db.Stores.SingleOrDefault(s => s.Id == store.Id);
            entity.Name = store.Name.Trim();
            entity.IsVirtual = store.IsVirtual;
            entity.CountryId = store.CountryId;
            entity.CityId = store.CityId;
            _db.Update(entity);
            _db.SaveChanges();
            return Success("Store updated successfully.");
        }

        public Service Delete(int id)
        {
            Store entity = _db.Stores.Include(s => s.ProductStores).SingleOrDefault(s => s.Id == id);
            _db.ProductStores.RemoveRange(entity.ProductStores);
            _db.Remove(entity);
            _db.SaveChanges();
            return Success("Store deleted successfully.");
        }
    }
}
