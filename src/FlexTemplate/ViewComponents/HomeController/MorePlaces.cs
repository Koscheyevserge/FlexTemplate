using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlexTemplate.Database;
using FlexTemplate.ViewModels.HomeController;
using FlexTemplate.Services;

namespace FlexTemplate.ViewComponents.HomeController
{
    public class MorePlaces : ViewComponent
    {
        private readonly Context _context;

        public MorePlaces(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(IEnumerable<int> loadedPlacesIds)
        {
            var ids = _context.Places.Where(p => !loadedPlacesIds.Contains(p.Id)).Select(p => p.Id).Except(loadedPlacesIds ?? new int[] { }).Take(8);
            var model = new LoadMorePlacesViewModel{ LoadedPlacesIds = ids};
            return View(model);
        }
    }
}
