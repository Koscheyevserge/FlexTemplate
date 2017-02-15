using System;
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
    public class OtherCityPlaces : ViewComponent
    {


        private readonly Context _context;

        public OtherCityPlaces(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {
            var photoPath = "images/othercityplaces/01.jpg";
            var cityName = _context.Cities.Where(city => city.Id == id).FirstOrDefault().Name;
            var placesCount = _context.Places.Include(place => place.Street).Where(place => place.Street.CityId == id).Count();
            var placeDescription = "заведений";
            
            var model = new OtherCityPlacesViewModel { PhotoPath = photoPath, CityName = cityName, PlacesCount = placesCount, PlaceDescriptor = placeDescription };
            return View(model);
        }


    }
}
