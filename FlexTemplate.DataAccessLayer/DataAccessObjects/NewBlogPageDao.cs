using System;
using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class NewBlogPageDao
    {
        public Guid NewBlogGuid { get; set; }
        public string BannerPhotoPath { get; set; }
        public List<NewBlogPageCategoryDao> Categories { get; set; }
    }
}
