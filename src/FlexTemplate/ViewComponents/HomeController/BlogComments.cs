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
    public class BlogComments : ViewComponent
    {
        private readonly Context _context;

        public BlogComments(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(IEnumerable<BlogComment> item)
        {
            return View(new HomeBlogCommentsViewModel { Comments = item });
        }
    }
}
