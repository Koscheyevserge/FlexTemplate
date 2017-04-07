using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FlexTemplate.Database;
using FlexTemplate.ViewModels.HomeController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FlexTemplate.Services;

namespace FlexTemplate.ViewComponents.HomeController
{
    public class ThisCityPlaces : ViewComponent
    {
        private readonly Context _context;

        public ThisCityPlaces(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string template, IEnumerable<int> loadedPlacesIds)
        {
            var ids = _context.Places.Where(p => !loadedPlacesIds.Contains(p.Id)).Select(p => p.Id).Except(loadedPlacesIds == null ? new int[] {} : loadedPlacesIds).Take(8).ToList();
            if (loadedPlacesIds != null) ids = ids.Concat(loadedPlacesIds).ToList();
            var strings = LocalizableStringsProvider.GetStrings(_context, GetType().Name, User.IsInRole("Supervisor"));
            var model = new ThisCityPlacesViewModel {ThisCityPlaceIds = ids.ToList(), Strings = strings};
            return View(template, model);
        }
    }
}
