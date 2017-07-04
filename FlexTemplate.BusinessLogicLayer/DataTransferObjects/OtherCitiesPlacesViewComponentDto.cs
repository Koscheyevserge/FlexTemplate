using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class OtherCitiesPlacesViewComponentDto
    {
        public IEnumerable<int> OtherCitiesPlacesIds { get; set; }
        public string TitleLabelCaption { get; set; }
        public string SubtitleLabelCaption { get; set; }
    }
}
