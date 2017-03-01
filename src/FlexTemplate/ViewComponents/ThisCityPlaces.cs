using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.ViewModels;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public IViewComponentResult Invoke(string template)
        {
            var ids = _context.Places.Take(8).Select(p => p.Id).ToList();
            var strings = _context.Containers.Include(c => c.LocalizableStrings)
                .FirstOrDefault(c => c.Name == GetType().Name)
                .LocalizableStrings.ToDictionary(ls => ls.Tag, ls => ls.Text);
            var model = new ThisCityPlacesViewModel {ThisCityPlaceIds = ids, Strings = strings};
            return View(template, model);
        }
    }
}
