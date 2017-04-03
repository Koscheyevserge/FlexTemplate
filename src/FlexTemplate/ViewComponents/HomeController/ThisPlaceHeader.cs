using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using Microsoft.AspNetCore.Mvc;
using FlexTemplate.Entities;
using FlexTemplate.ViewModels.HomeController;

namespace FlexTemplate.ViewComponents.HomeController
{
    public class ThisPlaceHeader : ViewComponent
    {
        private readonly Context _context;

        public ThisPlaceHeader(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(Place item)
        {
            return View(new ThisPlaceHeaderViewModel { HeadPhoto = $"Resources/Places/{item.Id}/head.jpg", Place = item , Stars = item.Reviews.Any() ? Math.Ceiling(item.Reviews.Where(p => p.Star > 0).Average(p => p.Star)) : 0 });
        }
    }
}