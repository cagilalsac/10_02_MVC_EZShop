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
    public class CountriesController : MvcController
    {
        // Service injections:
        private readonly ICountryService _countryService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public CountriesController(
			ICountryService countryService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _countryService = countryService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: Countries
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _countryService.Query().ToList();
            return View(list);
        }

        // GET: Countries/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _countryService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            
            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Countries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CountryModel country)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _countryService.Create(country.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = country.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(country);
        }

        // GET: Countries/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _countryService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Countries/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CountryModel country)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _countryService.Update(country.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = country.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(country);
        }

        // GET: Countries/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _countryService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Countries/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _countryService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
