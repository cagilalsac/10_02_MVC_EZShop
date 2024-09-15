#nullable disable

using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface ICityService
    {
        public IQueryable<CityModel> Query();
        public Service Create(City city);
        public Service Update(City city);
        public Service Delete(int id);

        public List<CityModel> GetList(int? countryId);
    }

    public class CityService : Service, ICityService
    {
        public CityService(Db db) : base(db)
        {
        }

        public IQueryable<CityModel> Query()
        {
            return _db.Cities.Include(c => c.Country).OrderBy(c => c.Name).Select(c => new CityModel() { Record = c });
        }

        public Service Create(City city)
        {
            if (_db.Cities.Any(c => c.Name.ToUpper() == city.Name.ToUpper().Trim()))
                return Error("City with the same name exists!");
            city.Name = city.Name.Trim();
            _db.Cities.Add(city);
            _db.SaveChanges();
            return Success("City created successfully.");
        }

        public Service Update(City city)
        {
            if (_db.Cities.Any(c => c.Id != city.Id && c.Name.ToUpper() == city.Name.ToUpper().Trim()))
                return Error("City with the same name exists!");
            var entity = _db.Cities.SingleOrDefault(c => c.Id == city.Id);
            entity.Name = city.Name.Trim();
            entity.CountryId = city.CountryId;
            _db.Cities.Update(entity);
            _db.SaveChanges();
            return Success("City updated successfully.");
        }

        public Service Delete(int id)
        {
            City entity = _db.Cities.Include(c => c.Stores).SingleOrDefault(c => c.Id == id);
            if (entity.Stores.Any())
                return Error("City has relational stores!");
            _db.Cities.Remove(entity);
            _db.SaveChanges();
            return Success("City deleted successfully.");
        }

        public List<CityModel> GetList(int? countryId)
        {
            return countryId.HasValue ? _db.Cities.OrderBy(c => c.Name).Where(c => c.CountryId == countryId).Select(c => new CityModel() 
            { 
                Record = c 
            }).ToList() : new List<CityModel>();
        }
    }
}