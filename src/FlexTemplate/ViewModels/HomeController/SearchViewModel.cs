using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.ViewModels
{
    public class SearchViewModel : EditableViewModel
    {
        public string PhotoPath { get; set; }
        public List<string> CategoriesNames { get; set; }
    }
}
