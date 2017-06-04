using Microsoft.AspNetCore.Html;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Components.CityPlaces
{
    public class ViewModel
    {
        public IEnumerable<int> ThisCityPlaceIds { get; set; }
        public HtmlString MorePlacesButtonCaption { get; set; }
        public HtmlString TitleLabelCaption { get; set; }
        public HtmlString SubtitleLabelCaption { get; set; }
    }
}
