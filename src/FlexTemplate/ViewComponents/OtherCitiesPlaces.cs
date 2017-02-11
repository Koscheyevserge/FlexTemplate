﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.ViewComponents
{
    public class OtherCitiesPlaces : ViewComponent
    {
        public IViewComponentResult Invoke(OtherCitiesPlacesViewModel model)
        {
            return View(model);
        }
    }
}