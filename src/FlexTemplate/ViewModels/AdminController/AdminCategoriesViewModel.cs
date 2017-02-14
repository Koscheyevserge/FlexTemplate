using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;

namespace FlexTemplate.ViewModels.AdminController
{
    public class AdminCategoriesViewModel
    {
        public IEnumerable<Language> Languages { get; set; }
        public IEnumerable<Category> Categories { get; set; } 
    }
}
