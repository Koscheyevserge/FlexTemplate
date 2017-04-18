using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using Microsoft.AspNetCore.Mvc;
using FlexTemplate.Entities;
using FlexTemplate.ViewModels.HomeController;

namespace FlexTemplate.ViewComponents.HomeController
{
    public class BlogsFeed : ViewComponent
    {
        private readonly Context _context;

        public BlogsFeed(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(Blog item)
        {
            return View(new BlogsFeedViewModel { Blog = item });
        }
    }
}
