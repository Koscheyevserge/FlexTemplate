using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class EditBlogPageCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}
