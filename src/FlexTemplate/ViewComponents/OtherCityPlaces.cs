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

namespace FlexTemplate.ViewComponents
{
    public class OtherCityPlaces : ViewComponent
    {


        private readonly Context _context;

        public OtherCityPlaces(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var photoPath = "images/othercityplaces/01.jpg";
            var cityName = "Київ";
            var placesCount = 20;
            var placeDescription = "заведений";
            
            var model = new OtherCityPlacesViewModel { PhotoPath = photoPath, CityName = cityName, PlacesCount = placesCount, PlaceDescriptor = placeDescription };
            return View(model);
        }


    }
}
