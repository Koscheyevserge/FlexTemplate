using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.YouMayAlsoLike
{
    public class YouMayAlsoLike : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();/*
            return View(new YouMayAlsoLikeViewModel
            {
                Places = _context.Places.Except(new List<Place> {item})
                .Include(p => p.Street)
                .ThenInclude(s => s.City)
                .Include(p => p.Reviews)
                .Include(p => p.PlaceCategories)
                .ThenInclude(pc => pc.Category)
                .Take(4)
            });*/
        }
    }
}
