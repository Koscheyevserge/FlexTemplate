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

        public IViewComponentResult Invoke()
        {
            return View(new BlogsFeedViewModel
            {
                Tags = _context.Tags
                    .Include(t => t.BlogTags)
                    .Select(t => 
                        new BlogsTagItem
                        {
                            BlogsCount = t.BlogTags.Count,
                            Caption = t.Name,
                            Id = t.Id
                        }),
                Blogs = _context.Blogs
                .Include(blog => blog.Comments)
                .Select(b => new BlogsFeedItem { Caption = b.Caption, Id = b.Id, CommentsCount = b.Comments.Count })
                .OrderByDescending(b => b.CommentsCount)
                .Take(4),
                LatestBlogs = _context.Blogs.OrderByDescending(b => b.CreatedOn)
                .Select(b =>new BlogsFeedLatestItem{Caption = b.Caption,Id = b.Id,CreatedOn = b.CreatedOn.ToShortDateString()})
                .Take(5)
            });
        }
    }
}
