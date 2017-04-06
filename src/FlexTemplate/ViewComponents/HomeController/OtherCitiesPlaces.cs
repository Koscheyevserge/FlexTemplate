using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using FlexTemplate.Database;
using FlexTemplate.Services;
using FlexTemplate.ViewModels.HomeController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.ViewComponents.HomeController
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
            var strings = LocalizableStringsProvider.GetStrings(_context, GetType().Name, User.IsInRole("Supervisor"));
            var model = new OtherCitiesPlacesViewModel { OtherCitiesPlacesIds = ids, Strings = strings };
            return View(template, model);
        }
    }
}
