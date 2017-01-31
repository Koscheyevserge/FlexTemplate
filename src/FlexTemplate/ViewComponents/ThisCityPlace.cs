using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlexTemplate.ViewModels;

namespace FlexTemplate.ViewComponents
{
    public class ThisCityPlace : ViewComponent
    {
        public IViewComponentResult Invoke(ThisCityPlaceViewModel model)
        {
            return View(model);
        }
    }
}
