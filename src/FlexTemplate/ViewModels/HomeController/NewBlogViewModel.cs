using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.ViewModels.HomeController
{
    public class NewBlogViewModel
    {
        public string Tags { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Preamble { get; set; }
        public Guid Guid { get; set; }
    }
}
