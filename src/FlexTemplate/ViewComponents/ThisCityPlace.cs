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
    public class ThisCityPlace : ViewComponent
    {
        private readonly Context _context;

        public ThisCityPlace(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var photoPath = "images/hot-item/01.jpg";
            var name = "Фламінго";
            var address = "Вул. Симоненка 1";
            var stars = 3;
            var reviewsCount = 27;
            var categories = _context.Categories.Select(c => c.Name).ToList();

            var model = new ThisCityPlaceViewModel { PhotoPath = photoPath, Name = name, Address = address, Stars = stars, ReviewsCount = reviewsCount, Categories = categories  };
            return View(model);
        }
    }
}
