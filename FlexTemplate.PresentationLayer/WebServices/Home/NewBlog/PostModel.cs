using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Home.NewBlog
{
    public class PostModel
    {
        public string Tags { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public Guid BannersKey { get; set; }
    }
}
