using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Components.BlogsFeed
{
    public class PopularBlogViewModel
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public int CommentsCount { get; set; }
        public string HeadPhotoPath { get; set; }
    }
}
