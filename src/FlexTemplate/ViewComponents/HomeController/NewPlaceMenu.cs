using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.ViewModels.HomeController;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.ViewComponents.HomeController
{
    public class NewPlaceMenu : ViewComponent
    {
        public IViewComponentResult Invoke(NewPlaceNewMenuViewModel model)
        {
            return View(model);
        }
    }
}
