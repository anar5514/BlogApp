using BlogApp.Data.Abstracts;
using BlogApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Data.Concrete.EfCore
{
    public class EfBlogRepository : IBlogRepository
    {
        private BlogContext _context;
        public EfBlogRepository(BlogContext context)
        {
            _context = context;
        }

        public void AddBlog(Blog blog)
        {
            _context.Blogs.Add(blog);
            _context.SaveChanges();
        }

        public void DeleteBlog(int id)
        {
            var entity = _context.Blogs.FirstOrDefault(x => x.Id == id);

            if(entity != null)
            {
                _context.Blogs.Remove(entity);
                _context.SaveChanges();
            }
        }

        public IQueryable<Blog> GetAll()
        {
            return _context.Blogs.AsQueryable();
        }

        public Blog GetById(int id)
        {
            return _context.Blogs.FirstOrDefault(x => x.Id == id);
        }

        public void SaveBlog(Blog blog)
        {
            if(blog.Id == 0)
            {
                blog.Date = DateTime.Now;
                _context.Blogs.Add(blog);
            }
            else
            {
                var entity = GetById(blog.Id);

                entity.ImagePath = blog.ImagePath;
                entity.Title = blog.Title;
                entity.Description = blog.Description;
                entity.Body = blog.Body;
                entity.CategoryId = blog.CategoryId;
                entity.Body = blog.Body;
                entity.IsApproved = blog.IsApproved;
                entity.IsHome = blog.IsHome;
                entity.IsSlider = blog.IsSlider;
            }

            _context.SaveChanges();
        }

    }
}
