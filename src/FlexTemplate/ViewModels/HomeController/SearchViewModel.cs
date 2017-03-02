using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.ViewModels
{
    public class SearchViewModel : EditableViewModel
    {
        public List<string> Images { get; set; }
        public List<string> CategoriesNames { get; set; }
        public Dictionary<string, string> Strings { get; set; }
    }
}
