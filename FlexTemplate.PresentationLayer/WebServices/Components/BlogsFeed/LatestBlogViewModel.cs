using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Components.BlogsFeed
{
    public class LatestBlogViewModel
    {
        public string HeadPhotoPath { get; set; }
        public string Caption { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Id { get; set; }
    }
}
