using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Components.BlogsFeed
{
    public class ViewModel
    {
        public IEnumerable<LatestBlogViewModel> LatestBlogs { get; set; }
        public IEnumerable<PopularBlogViewModel> Blogs { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public IEnumerable<TagViewModel> Tags { get; set; }
        public string Input { get; set; }
        public IEnumerable<int> TagsIds { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<int> CategoriesIds { get; set; }
    }
}
