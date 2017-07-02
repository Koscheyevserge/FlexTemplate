using System;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class CachedBlogItemDto
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
