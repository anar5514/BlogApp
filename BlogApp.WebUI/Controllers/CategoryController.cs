using BlogApp.Data.Abstracts;
using BlogApp.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository _repo;
        public CategoryController(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            return View(_repo.GetAll());
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_repo.GetById(id));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.DeleteCategory(id);
            TempData["message"] = $"{id} numarali kayit silindi.";
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult AddOrUpdate(int? id)
        {
            ViewBag.Categories = new SelectList(_repo.GetAll(), "Id", "Name");

            if (id == null)
            {
                return View(new Category());
            }

            return View(_repo.GetById((int)id));
        }
        [HttpPost]
        public IActionResult AddOrUpdate(Category category)
        {
            if (ModelState.IsValid)
            {
                _repo.SaveCategory(category);
                TempData["message"] = $"{category.Name} kayit edildi.";
                return RedirectToAction("List");
            }

            ViewBag.Categories = new SelectList(_repo.GetAll(), "Id", "Name");
            return View(category);
        }
    }
}
