using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlexTemplate.ViewModels;

namespace FlexTemplate.ViewComponents
{
    public class ThisCityPlaces : ViewComponent
    {
        public IViewComponentResult Invoke(ThisCityPlacesViewModel model)
        {
            return View(model);
        }
    }
}
