using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;

namespace FlexTemplate.PresentationLayer.WebServices.Components.OtherCitiesPlaces
{
    public class ViewModel
    {
        public IEnumerable<int> OtherCitiesPlacesIds { get; set; }
        public HtmlString TitleLabelCaption { get; set; }
        public HtmlString SubtitleLabelCaption { get; set; }
    }
}
