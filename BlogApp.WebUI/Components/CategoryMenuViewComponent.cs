using BlogApp.Data.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.WebUI.Components
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private ICategoryRepository _categoryRepository;
        public CategoryMenuViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IViewComponentResult Invoke(ICategoryRepository categoryRepository)
        {
            return View(_categoryRepository.GetAll());
        }
    }
}
