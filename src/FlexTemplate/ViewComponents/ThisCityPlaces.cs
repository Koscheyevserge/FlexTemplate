﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.ViewModels;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FlexTemplate.ViewComponents
{
    public class ThisCityPlaces : ViewComponent
    {
        private readonly Context _context;

        public ThisCityPlaces(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var model = new ThisCityPlacesViewModel { };
            return View(model);
        }
    }
}
