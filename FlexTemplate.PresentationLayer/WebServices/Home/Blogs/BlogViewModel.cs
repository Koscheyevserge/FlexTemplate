using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Home.Blogs
{
    public class BlogViewModel
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
