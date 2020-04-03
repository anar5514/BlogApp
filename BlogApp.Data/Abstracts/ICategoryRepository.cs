using BlogApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Data.Abstracts
{
    public interface ICategoryRepository 
    {
        IQueryable<Category> GetAll();
        Category GetById(int id);
        void DeleteCategory(int id);
        void SaveCategory(Category Category); 
        void AddCategory(Category category);
    }
}
