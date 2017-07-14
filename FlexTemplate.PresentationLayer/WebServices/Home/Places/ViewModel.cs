using System.Collections.Generic;
using FlexTemplate.PresentationLayer.Core;

namespace FlexTemplate.PresentationLayer.WebServices.Home.Places
{
    public class ViewModel : FlexPageViewModel
    {
        public IEnumerable<int> PlacesOnPageIds { get; set; }
        public int PlacesTotal { get; set; }
        public string BannerPhotoPath { get; set; }
    }
}
