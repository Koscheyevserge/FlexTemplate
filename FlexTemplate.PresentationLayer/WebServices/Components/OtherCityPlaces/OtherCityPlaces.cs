using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.OtherCityPlaces
{
    public class OtherCityPlaces : ViewComponent
    {
        public IViewComponentResult Invoke(int id)
        {
            return View();
            /*var photoPath = "images/3.jpg";
            var city = _context.Cities.FirstOrDefault(c => c.Id == id);
            var cityName = city != null ? city.Name : string.Empty;
            var placesCount = _context.Places.Include(place => place.Street).Count(place => place.Street.CityId == id);
            var placeDescription = "закладів";
            var model = new OtherCityPlacesViewModel { CityId = city.Id, PhotoPath = photoPath, CityName = cityName, PlacesCount = placesCount, PlaceDescriptor = placeDescription };
            return View(model);*/
        }
    }
}
