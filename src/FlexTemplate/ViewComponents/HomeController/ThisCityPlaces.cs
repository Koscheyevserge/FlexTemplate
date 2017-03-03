using System.Linq;
using FlexTemplate.Database;
using FlexTemplate.ViewModels.HomeController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.ViewComponents.HomeController
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
