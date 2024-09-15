#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;

// Generated from Custom Template.

namespace MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CitiesController : MvcController
    {
        // Service injections:
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public CitiesController(
			ICityService cityService
            , ICountryService countryService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _cityService = cityService;
            _countryService = countryService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
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
            
            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
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
            var cities = _cityService.GetList(countryId);
            return Json(cities.Select(c => new
            {
                value = c.Record.Id,
                text = c.Name
            }));
        }
    }
}
