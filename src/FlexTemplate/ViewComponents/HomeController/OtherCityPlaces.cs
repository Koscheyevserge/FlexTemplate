using System.Linq;
using FlexTemplate.Database;
using FlexTemplate.ViewModels.HomeController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.ViewComponents.HomeController
{
    public class OtherCityPlaces : ViewComponent
    {
        private readonly Context _context;

        public OtherCityPlaces(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {
            var photoPath = "images/3.jpg";
            var city = _context.Cities.FirstOrDefault(c => c.Id == id);
            var cityName = city != null ? city.Name : string.Empty;
            var placesCount = _context.Places.Include(place => place.Street).Count(place => place.Street.CityId == id);
            var placeDescription = "закладів";
            var model = new OtherCityPlacesViewModel { CityId = city.Id, PhotoPath = photoPath, CityName = cityName, PlacesCount = placesCount, PlaceDescriptor = placeDescription };
            return View(model);
        }
    }
}
