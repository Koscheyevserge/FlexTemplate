using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlaceOverview
{
    public class PlaceOverview : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(); //return View(new ThisPlaceOverviewViewModel {Place = item});
        }
    }
}