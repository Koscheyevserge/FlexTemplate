using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;

namespace FlexTemplate.ViewModels.HomeController
{
    public class ThisPlaceHeaderViewModel
    {
        public Place Place { get; set; }
        public double Stars { get; set; }
        public bool CanEdit { get; set; }
    }
}
