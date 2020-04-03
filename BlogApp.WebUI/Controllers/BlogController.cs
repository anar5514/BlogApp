using BlogApp.Data.Abstracts;
using BlogApp.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private IBlogRepository _blogRepository;
        private ICategoryRepository _categoryRepository;
        public BlogController(IBlogRepository blogRepository, ICategoryRepository categoryRepository)
        {
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Index(int? id, string q)
        {
            var query = _blogRepository.GetAll().Where(x => x.IsApproved);

            if (id != null)
            {
                query = query.Where(x => x.CategoryId == id);
            }

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(x => EF.Functions.Like(x.Title, "%" + q + "%")
                || EF.Functions.Like(x.Description, "%" + q + "%")
                || EF.Functions.Like(x.Body, "%" + q + "%"));
            }

            ViewBag.SelectedCategoryId = RouteData?.Values["Id"];
            return View(query.OrderByDescending(x => x.Date));
        }

        // Edit Actions
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "Id", "Name");

            return View(_blogRepository.GetById((int)id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Blog blog, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\img\\{file.FileName}");

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    blog.ImagePath = file.FileName;
                }
                _blogRepository.SaveBlog(blog);
                TempData["message"] = $"{blog.Title} kayıt edildi.";
                return RedirectToAction("List");
            }

            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            return View(blog);
        }

        //Create Actions
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "Id", "Name");

            return View();
        }
        [HttpPost]
        public IActionResult Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                _blogRepository.SaveBlog(blog);
                TempData["message"] = $"{blog.Title} kayıt edildi.";
                return RedirectToAction("List");
            }

            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            return View(blog);
        }


        //Delete Actions
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_blogRepository.GetById(id));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _blogRepository.DeleteBlog(id);
            TempData["message"] = $"{id} numarali kayit silindi.";
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            return View(_blogRepository.GetAll());
        }

        public IActionResult Details(int id)
        {
            return View(_blogRepository.GetById(id));
        }
    }
}
