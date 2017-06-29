using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class BlogsFeedComponentDto
    {
        public IEnumerable<BlogsFeedComponentLatestBlogDto> LatestBlogs { get; set; }
        public IEnumerable<BlogsFeedComponentPopularBlogDto> Blogs { get; set; }
        public IEnumerable<BlogsFeedComponentCategoryDto> Categories { get; set; }
        public IEnumerable<BlogsFeedComponentTagDto> Tags { get; set; }
    }
}
