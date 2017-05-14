using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.ViewModels.HomeController
{
    public class BlogsFeedViewModel
    {
        public IEnumerable<BlogsFeedItem> Blogs { get; set; }
        public IEnumerable<BlogsFeedLatestItem> LatestBlogs { get; set; }
        public IEnumerable<BlogsTagItem> Tags { get; set; }
    }

    public class BlogsTagItem
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public int BlogsCount { get; set; }
    }

    public class BlogsFeedItem
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public int CommentsCount { get; set; }
    }

    public class BlogsFeedLatestItem
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string CreatedOn { get; set; }
    }
}
