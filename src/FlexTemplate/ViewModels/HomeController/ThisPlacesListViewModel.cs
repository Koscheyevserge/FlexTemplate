using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;

namespace FlexTemplate.ViewModels.HomeController
{
    public class ThisPlacesListViewModel
    {
        public IEnumerable<Place> Places { get; set; } 
    }
}
