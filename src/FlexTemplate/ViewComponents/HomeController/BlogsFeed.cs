using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using Microsoft.AspNetCore.Mvc;
using FlexTemplate.Entities;
using FlexTemplate.ViewModels.HomeController;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.ViewComponents.HomeController
{
    public class BlogsFeed : ViewComponent
    {
        private readonly Context _context;

        public BlogsFeed(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(IEnumerable<Blog> exceptBlogs)
        {
            return View(new BlogsFeedViewModel {Blogs = _context.Blogs.Include(blog => blog.Comments).Except(exceptBlogs).Take(4)});
        }
    }
}
