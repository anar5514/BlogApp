using BlogApp.Data.Abstracts;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IBlogRepository _repo;

        public HomeController(IBlogRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            HomeBlogModel homeBlogModel = new HomeBlogModel();
            homeBlogModel.HomeBlogs = new List<Blog>(_repo.GetAll().Where(x => x.IsApproved && x.IsHome).ToList());
            homeBlogModel.SliderBlogs = new List<Blog>(_repo.GetAll().Where(x => x.IsApproved && x.IsSlider).ToList());

            return View(homeBlogModel);
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
