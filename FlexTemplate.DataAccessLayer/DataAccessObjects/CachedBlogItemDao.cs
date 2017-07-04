using System;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class CachedBlogItemDao
    {
        public string HeadPhotoPath { get; set; }
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string AuthorName { get; set; }
        public bool IsModerated { get; set; }
        public string Caption { get; set; }
        public string Preable { get; set; }
    }
}
