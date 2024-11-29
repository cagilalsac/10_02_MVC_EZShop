#nullable disable
using BLL.Controllers.Bases;
using BLL.DAL;
using BLL.Models;
using BLL.Services;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

// Generated from Custom Template.

namespace MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : MvcController
    {
        // Service injections:
        private readonly IService<User, UserModel> _userService;
        private readonly IService<Role, RoleModel> _roleService;

        /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
        //private readonly IService<{Entity}, {Entity}Model> _{Entity}Service;

        public UsersController(
            IService<User, UserModel> userService
            , IService<Role, RoleModel> roleService

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //, IService<{Entity}, {Entity}Model> {Entity}Service
        )
        {
            _userService = userService;
            _roleService = roleService;

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //_{Entity}Service = {Entity}Service;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserModel user)
        {
            ModelState.Remove("Record.RoleId");
            if (ModelState.IsValid)
            {
                var existingUser = _userService.Query().SingleOrDefault(q => q.Record.UserName == user.Record.UserName && q.Record.Password == user.Record.Password && q.Record.IsActive);
                if (existingUser is not null)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim("Id", existingUser.Record.Id.ToString()),
                        new Claim(ClaimTypes.Name, existingUser.UserName),
                        new Claim(ClaimTypes.Role, existingUser.Role)
                    };
                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid user name or password!");
            }
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Register(UserModel user)
        {
            ModelState.Remove("Record.RoleId");
            if (ModelState.IsValid)
            {
                var userService = _userService as UserService;
                var result = userService.Register(user.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Login));
                }
                ModelState.AddModelError("", result.Message);
            }
            return View();
        }

        // GET: Users
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _userService.Query().ToList();
            return View(list);
        }

        // GET: Users/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _userService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Record.Id", "RoleName");
            
            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //ViewBag.{Entity}Ids = new MultiSelectList(_{Entity}Service.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            SetViewData();
            var user = new UserModel()
            {
                Record = new User()
                {
                    IsActive = true,
                    RoleId = (int)Roles.User
                }
            };
            return View(user);
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserModel user)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _userService.Create(user.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = user.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(user);
        }

        // GET: Users/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _userService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Users/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserModel user)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _userService.Update(user.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = user.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(user);
        }

        // GET: Users/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _userService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Users/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _userService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
