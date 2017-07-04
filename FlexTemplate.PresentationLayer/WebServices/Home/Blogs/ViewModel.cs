using System.Collections;
using System.Collections.Generic;

namespace FlexTemplate.PresentationLayer.WebServices.Home.Blogs
{
    public class ViewModel
    {
        public string HeaderPhotoPath { get; set; }
        public IEnumerable<BlogViewModel> Blogs { get; set; }
        public int BlogsCount { get; set; }
    }
}
