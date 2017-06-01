using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlacesFilters
{
    public class PlacesFilters : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = new ViewModel();// {Categories = _context.Categories, Cities = _context.Cities, SelectedCategories = categories, SelectedCities = cities};
            return View(model);
        }
    }
}
