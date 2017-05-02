using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;

namespace FlexTemplate.ViewModels.HomeController
{
    public class BlogsFeedViewModel
    {
        public virtual IEnumerable<BlogsFeedItem> Blogs { get; set; }
    }

    public class BlogsFeedItem
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public int CommentsCount { get; set; }
    }
}
