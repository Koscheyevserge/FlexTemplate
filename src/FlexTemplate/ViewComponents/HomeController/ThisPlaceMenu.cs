﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using Microsoft.AspNetCore.Mvc;
using FlexTemplate.Entities;

namespace FlexTemplate.ViewComponents.HomeController
{
    public class ThisPlaceMenu : ViewComponent
    {
        private readonly Context _context;

        public ThisPlaceMenu(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(Place item)
        {
            return View();
        }
    }
}