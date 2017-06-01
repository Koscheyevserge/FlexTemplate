using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.OtherCitiesPlaces
{
    public class OtherCitiesPlaces : ViewComponent
    {

        public IViewComponentResult Invoke(string template)
        {
            //var ids = _context.Cities.Take(4).Select(city => city.Id).ToList();
            //var strings = LocalizableStringsProvider.GetStrings(_context, GetType().Name, User.IsInRole("Supervisor"));
            //var model = new OtherCitiesPlacesViewModel { OtherCitiesPlacesIds = ids, Strings = strings };
            return View();//(template, model);
        }
    }
}
