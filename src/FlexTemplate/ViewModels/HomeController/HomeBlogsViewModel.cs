using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;

namespace FlexTemplate.ViewModels.HomeController
{
    public class HomeBlogsViewModel
    {
        public DateTime CreatedOn { get; set; }
        public string Caption { get; set; }
        public User Author { get; set; }
        public virtual List<BlogComment> Comments { get; set; }
        public string Preamble { get; set; }
        public string Text { get; set; }
    }
}
