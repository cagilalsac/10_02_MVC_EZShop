#nullable disable
using BLL.Controllers.Bases;
using BLL.DAL;
using BLL.Models;
using BLL.Services;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// Generated from Custom Template.

namespace MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CitiesController : MvcController
    {
        // Service injections:
        private readonly IService<City, CityModel> _cityService;
        private readonly IService<Country, CountryModel> _countryService;

        /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
        //private readonly IService<{Entity}, {Entity}Model> _{Entity}Service;

        public CitiesController(
            IService<City, CityModel> cityService
            , IService<Country, CountryModel> countryService

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //, IService<{Entity}, {Entity}Model> {Entity}Service
        )
        {
            _cityService = cityService;
            _countryService = countryService;

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //_{Entity}Service = {Entity}Service;
        }

        // GET: Cities
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _cityService.Query().ToList();
            return View(list);
        }

        // GET: Cities/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _cityService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["CountryId"] = new SelectList(_countryService.Query().ToList(), "Record.Id", "Name");
            
            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //ViewBag.{Entity}Ids = new MultiSelectList(_{Entity}Service.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Cities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CityModel city)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _cityService.Create(city.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = city.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(city);
        }

        // GET: Cities/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _cityService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Cities/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CityModel city)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _cityService.Update(city.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = city.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(city);
        }

        // GET: Cities/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _cityService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Cities/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _cityService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        // GET: Cities/Get?countryId=1
        public JsonResult Get(int? countryId)
        {
            var cityService = _cityService as CityService;
            var cities = cityService.GetList(countryId);
            return Json(cities.Select(c => new
            {
                value = c.Record.Id,
                text = c.Name
            }));
        }
    }
}
