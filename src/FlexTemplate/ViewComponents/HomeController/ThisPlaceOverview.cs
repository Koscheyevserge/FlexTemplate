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
    public class ThisPlaceOverview : ViewComponent
    {
        private readonly Context _context;

        public ThisPlaceOverview(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(Place item)
        {
            return View(new ThisPlaceOverviewViewModel {Place = item});
        }
    }
}