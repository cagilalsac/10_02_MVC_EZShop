using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IUserService
    {
        public UserModel LoggedInUser { get; set; }
        public Service Register(User user);
        public Service Login(User user);

        public IQueryable<UserModel> Query();
        public Service Create(User user);
        public Service Update(User user);
        public Service Delete(int id);
    }

    public class UserService : Service, IUserService
    {
        public UserModel LoggedInUser { get; set; }

        public UserService(Db db) : base(db)
        {
        }

        public Service Register(User user)
        {
            if (_db.Users.Any(u => u.UserName == user.UserName.Trim()))
                return Error("User with the same user name exists!");
            user.UserName = user.UserName.Trim();
            user.Password = user.Password.Trim();
            user.IsActive = true;
            user.RoleId = (int)Roles.User;
            _db.Users.Add(user);
            _db.SaveChanges();
            return Success("User registered successfully.");
        }

        public Service Login(User user)
        {
            var entity = _db.Users.Include(u => u.Role).SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password && u.IsActive);
            if (entity is null)
                return Error("Invalid user name and password!");
            LoggedInUser = new UserModel()
            {
                Record = entity
            };
            return Success("User logged in successfully.");
        }

        public IQueryable<UserModel> Query()
        {
            return _db.Users.Include(u => u.Role).OrderByDescending(u => u.IsActive).ThenBy(u => u.UserName).Select(u => new UserModel() { Record = u });
        }

        public Service Create(User user)
        {
            if (_db.Users.Any(u => u.UserName == user.UserName.Trim()))
                return Error("User with the same user name exists!");
            user.UserName = user.UserName.Trim();
            user.Password = user.Password.Trim();
            user.IsActive = user.IsActive;
            user.RoleId = user.RoleId;
            _db.Users.Add(user);
            _db.SaveChanges();
            return Success("User created successfully.");
        }

        public Service Update(User user)
        {
            if (_db.Users.Any(u => u.Id != user.Id && u.UserName == user.UserName.Trim()))
                return Error("User with the same user name exists!");
            var entity = _db.Users.SingleOrDefault(u => u.Id == user.Id);
            entity.UserName = user.UserName.Trim();
            entity.Password = user.Password.Trim();
            entity.IsActive = user.IsActive;
            entity.RoleId = user.RoleId;
            _db.Users.Update(entity);
            _db.SaveChanges();
            return Success("User updated successfully.");
        }

        public Service Delete(int id)
        {
            var entity = _db.Users.SingleOrDefault(u => u.Id == id);
            entity.IsActive = false;
            var result = Update(entity);
            if (!result.IsSuccessful)
                return result;
            return Success("User deleted successfully.");
        }
    }
}
