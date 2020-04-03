using BlogApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Data.Abstracts
{
    public interface IBlogRepository
    {
        IQueryable<Blog> GetAll();
        Blog GetById(int id);
        void DeleteBlog(int id);
        void AddBlog(Blog blog);
        void SaveBlog(Blog blog);

    }
}
