using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.MorePlaces
{
    public class MorePlaces : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<int> loadedPlacesIds)
        {
            return View();
            //var ids = _context.Places.Where(p => !loadedPlacesIds.Contains(p.Id)).Select(p => p.Id).Except(loadedPlacesIds ?? new int[] { }).Take(8);
            //var model = new LoadMorePlacesViewModel{ LoadedPlacesIds = ids};
            //return View(model);
        }
    }
}
