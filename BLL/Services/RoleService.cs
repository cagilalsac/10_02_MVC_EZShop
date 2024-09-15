using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IRoleService
    {
        public IQueryable<RoleModel> Query();
        public Service Create(Role role);
        public Service Update(Role role);
        public Service Delete(int id);
    }

    public class RoleService : Service, IRoleService
    {
        public RoleService(Db db) : base(db)
        {
        }

        public IQueryable<RoleModel> Query()
        {
            return _db.Roles.Select(r => new RoleModel() { Record = r });
        }

        public Service Create(Role role)
        {
            if (_db.Roles.Any(r => r.RoleName.ToUpper() == role.RoleName.ToUpper().Trim()))
                return Error("Role with the same name exists!");
            role.RoleName = role.RoleName.Trim();
            _db.Roles.Add(role);
            _db.SaveChanges();
            return Success("Role created successfully.");
        }

        public Service Update(Role role)
        {
            if (_db.Roles.Any(r => r.Id != role.Id && r.RoleName.ToUpper() == role.RoleName.ToUpper().Trim()))
                return Error("Role with the same name exists!");
            var entity = _db.Roles.SingleOrDefault(r => r.Id == role.Id);
            entity.RoleName = role.RoleName.Trim();
            _db.Roles.Update(entity);
            _db.SaveChanges();
            return Success("Role updated successfully.");
        }

        public Service Delete(int id)
        {
            var entity = _db.Roles.Include(r => r.Users).SingleOrDefault(r => r.Id == id);
            if (entity.Users.Any())
                return Error("Role has relational users!");
            _db.Roles.Remove(entity);
            _db.SaveChanges();
            return Success("Role deleted successfully.");
        }
    }
}
