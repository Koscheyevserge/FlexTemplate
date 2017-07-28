using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class EditBlogPageCategoryDao
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}
