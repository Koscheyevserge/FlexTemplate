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
    public class ThisCityPlace : ViewComponent
    {
        private readonly Context _context;

        public ThisCityPlace(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {
            var photoPath = "images/hot-item/01.jpg";
            var place = _context.Places.Where(p => p.Id == id).Include(p => p.Street).Include(p => p.PlaceCategories).ThenInclude(pc => pc.Category).FirstOrDefault();
            var name = place.Name;
            var address = place.Street.Name;
            var categories = place.PlaceCategories.Select(c => c.Category.Name).ToList();

            var model = new ThisCityPlaceViewModel { PhotoPath = photoPath, Name = name, Address = address, Categories = categories};
            return View(model);
        }
    }
}
