using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Home.EditBlog
{
    public class PostModel
    {
        public int Id { get; set; }
        public string[] Tags { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
