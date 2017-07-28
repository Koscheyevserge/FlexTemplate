using System;
using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class EditBlogPageDao
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public string BannerPhotoPath { get; set; }
        public string Tags { get; set; }
        public Guid BlobKey { get; set; }
        public List<EditBlogPageCategoryDao> Categories { get; set; }
    }
}
