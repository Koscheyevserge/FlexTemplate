﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class BlogBlogCategory : BaseEntity
    {
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        public int BlogCategoryId { get; set; }
        public BlogCategory BlogCategory { get; set; }
    }
}
