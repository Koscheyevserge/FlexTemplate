using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using FlexTemplate.ViewModels.HomeController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.ViewComponents.HomeController
{
    public class YouMayAlsoLike : ViewComponent
    {
        private readonly Context _context;

        public YouMayAlsoLike(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(Place item)
        {

            return View(new YouMayAlsoLikeViewModel
            {
                Places = _context.Places.Except(new List<Place> {item})
                .Include(p => p.Street)
                .ThenInclude(s => s.City)
                .Include(p => p.Reviews)
                .Include(p => p.PlaceCategories)
                .ThenInclude(pc => pc.Category)
                .Take(4)
            });
        }
    }
}
