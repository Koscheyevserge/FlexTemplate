using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class CachedBlogDto
    {
        public string HeaderPhotoPath { get; set; }
        public IEnumerable<CachedBlogItemDto> Blogs { get; set; }
        public int BlogsCount { get; set; }
    }
}
