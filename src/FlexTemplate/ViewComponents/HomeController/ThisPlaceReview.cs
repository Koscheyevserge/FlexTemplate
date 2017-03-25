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
    public class ThisPlaceReview : ViewComponent
    {
        private readonly Context _context;

        public ThisPlaceReview(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(PlaceReview item)
        {
            return View(new ThisPlaceReviewViewModel {Review = item});
        }
    }
}
