using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.CityPlaces
{
    public class CityPlaces : ViewComponent
    {
        public IViewComponentResult Invoke(string template)
        {
            return View();
            //var ids = _context.Places.Select(p => p.Id).Take(8);
            //var strings = LocalizableStringsProvider.GetStrings(_context, GetType().Name, User.IsInRole("Supervisor"));
            //var model = new ThisCityPlacesViewModel {ThisCityPlaceIds = ids.ToList(), Strings = strings};
            //return View(template, model);
        }
    }
}
