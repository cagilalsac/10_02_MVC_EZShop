using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class CountryService : Service, IService<Country, CountryModel>
    {
        public CountryService(Db db) : base(db)
        {
        }

        public IQueryable<CountryModel> Query()
        {
            return _db.Countries.OrderBy(c => c.Name).Select(c => new CountryModel() { Record = c });
        }

        public Service Create(Country country)
        {
            if (_db.Countries.Any(c => c.Name.ToUpper() == country.Name.ToUpper().Trim()))
                return Error("Country with the same name exists!");
            country.Name = country.Name.Trim();
            _db.Countries.Add(country);
            _db.SaveChanges();
            return Success("Country created successfully.");
        }

        public Service Update(Country country)
        {
            if (_db.Countries.Any(c => c.Id != country.Id && c.Name.ToUpper() == country.Name.ToUpper().Trim()))
                return Error("Country with the same name exists!");
            var entity = _db.Countries.SingleOrDefault(c => c.Id == country.Id);
            entity.Name = country.Name.Trim();
            _db.Countries.Update(entity);
            _db.SaveChanges();
            return Success("Country updated successfully.");
        }

        public Service Delete(int id)
        {
            Country entity = _db.Countries.Include(c => c.Cities).Include(c => c.Stores).SingleOrDefault(c => c.Id == id);
            if (entity.Cities.Any())
                return Error("Country has relational cities!");
            if (entity.Stores.Any())
                return Error("Country has relational stores!");
            _db.Countries.Remove(entity);
            _db.SaveChanges();
            return Success("Country deleted successfully.");
        }
    }
}
