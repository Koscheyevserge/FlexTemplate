using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class CityPlacesViewComponentDto
    {
        public IEnumerable<int> ThisCityPlaceIds { get; set; }
        public string MorePlacesButtonCaption { get; set; }
        public string TitleLabelCaption { get; set; }
        public string SubtitleLabelCaption { get; set; }
    }
}
