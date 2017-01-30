using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.ViewComponents
{
    public class OtherCityPlaces : ViewComponent
    {
        public IViewComponentResult Invoke(OtherCityPlacesViewModel model)
        {
            return View(model);
        }
    }
}
