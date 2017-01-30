using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.ViewModels
{
    public class SearchViewModel : EditableViewModel
    {
        public string PhotoPath { get; set; }
        public List<string> CategoriesNames { get; }

      	public SearchViewModel()
      	{
      		PhotoPath = "~/images/hero-header/01.jpg";
      		List<string> CategoriesNames = new List<string>();
      		CategoriesNames.Add("Cuisine Type");
      		CategoriesNames.Add("African");
      		CategoriesNames.Add("American");
      		CategoriesNames.Add("Italian");
      		CategoriesNames.Add("French");
      		CategoriesNames.Add("Indochinese");
      		CategoriesNames.Add("Halal");
      	}
    }
}
