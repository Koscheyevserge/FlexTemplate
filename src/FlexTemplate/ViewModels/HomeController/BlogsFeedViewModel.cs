﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;

namespace FlexTemplate.ViewModels.HomeController
{
    public class BlogsFeedViewModel
    {
        public virtual IEnumerable<Blog> Blogs { get; set; }
        public Blog Blog { get; set; }
        public virtual IEnumerable<BlogComment> Comments { get; set; }
    }
}
