using System.Collections.Generic;

namespace FlexTemplate.PresentationLayer.WebServices.Home.Blogs
{
    public class ViewModel
    {
        //public IEnumerable<Blog> Blogs { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public int TotalFoundBlogsCount { get; set; }
    }
}
