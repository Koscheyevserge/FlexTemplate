using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using FlexTemplate.ViewModels.HomeController;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.ViewComponents.HomeController
{
    public class ThisPlacesList : ViewComponent
    {
        private readonly Context _context;

        public ThisPlacesList(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(Place item)
        {
            return View(new ThisPlaceReviewsViewModel { Place = item });
        }
    }
}
