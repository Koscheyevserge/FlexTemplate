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
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.ViewComponents
{
    public class OtherCitiesPlaces : ViewComponent
    {
        private readonly Context _context;

        public OtherCitiesPlaces(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string template)
        {
            var ids = _context.Cities.Take(4).Select(city => city.Id).ToList();
            template = string.IsNullOrEmpty(template) ? "Default" : template;
            var strings = _context.Containers.Include(c => c.LocalizableStrings)
                .FirstOrDefault(c => c.Name == GetType().Name && c.TemplateName == template)
                .LocalizableStrings.ToDictionary(ls => ls.Tag, ls => ls.Text);
            var model = new OtherCitiesPlacesViewModel { OtherCitiesPlacesIds = ids, Strings = strings };
            return View(string.IsNullOrEmpty(template) ? "Default" : template, model);
        }
    }
}
