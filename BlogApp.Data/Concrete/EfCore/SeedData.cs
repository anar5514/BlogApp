using BlogApp.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetRequiredService<BlogContext>();

            context.Database.Migrate();

            if (!context.Categories.Any())
            {
                context.Categories.AddRange
                (
                    new Category() { Name = "Category 1" },
                    new Category() { Name = "Category 2" },
                    new Category() { Name = "Category 3" }
                );

                context.SaveChanges();
            }

            if (!context.Blogs.Any())
            {
                context.Blogs.AddRange
                (
                    new Blog()
                    {
                        Title = "Blog Title 1",
                        Description = "Blog Description 1",
                        Date = DateTime.Now.AddDays(-7),
                        Body = "Blog Body 1",
                        ImagePath = "1.jpg",
                        IsApproved = true,
                        CategoryId = 1
                    },
                    new Blog()
                    {
                        Title = "Blog Title 2",
                        Description = "Blog Description 2",
                        Date = DateTime.Now.AddDays(-5),
                        Body = "Blog Body 2",
                        ImagePath = "2.jpg",
                        IsApproved = true,
                        CategoryId = 1
                    },
                    new Blog()
                    {
                        Title = "Blog Title 3",
                        Description = "Blog Description 3",
                        Date = DateTime.Now.AddDays(-2),
                        Body = "Blog Body 3",
                        ImagePath = "3.jpg",
                        IsApproved = false,
                        CategoryId = 2
                    },
                    new Blog()
                    {
                        Title = "Blog Title 4",
                        Description = "Blog Description 4",
                        Date = DateTime.Now.AddDays(-2),
                        Body = "Blog Body 4",
                        ImagePath = "4.jpg",
                        IsApproved = false,
                        CategoryId = 2
                    }
                );

                context.SaveChanges();
            }

        }
    }
}
