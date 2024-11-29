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
    public class StoresController : MvcController
    {
        // Service injections:
        private readonly IService<Store, StoreModel> _storeService;
        private readonly IService<City, CityModel> _cityService;
        private readonly IService<Country, CountryModel> _countryService;

        /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
        //private readonly IService<{Entity}, {Entity}Model> _{Entity}Service;

        public StoresController(
            IService<Store, StoreModel> storeService
            , IService<City, CityModel> cityService
            , IService<Country, CountryModel> countryService

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //, IService<{Entity}, {Entity}Model> {Entity}Service
        )
        {
            _storeService = storeService;
            _cityService = cityService;
            _countryService = countryService;

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //_{Entity}Service = {Entity}Service;
        }

        // GET: Stores
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _storeService.Query().ToList();
            return View(list);
        }

        // GET: Stores/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _storeService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData(int? countryId = null)
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            var cityService = _cityService as CityService;
            ViewData["CityId"] = new SelectList(cityService.GetList(countryId), "Record.Id", "Name");
            ViewData["CountryId"] = new SelectList(_countryService.Query().ToList(), "Record.Id", "Name");

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //ViewBag.{Entity}Ids = new MultiSelectList(_{Entity}Service.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            SetViewData();
            var store = new StoreModel()
            {
                Record = new Store()
                {
                    IsVirtual = true
                }
            };
            return View(store);
        }

        // POST: Stores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StoreModel store)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _storeService.Create(store.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = store.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(store);
        }

        // GET: Stores/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _storeService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData(item.Record.CountryId);
            return View(item);
        }

        // POST: Stores/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StoreModel store)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _storeService.Update(store.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = store.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData(store.Record.CountryId);
            return View(store);
        }

        // GET: Stores/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _storeService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Stores/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _storeService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
