using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class YouMayAlsoLikeComponentDto
    {
        public IEnumerable<YouMayAlsoLikeComponentPlaceDto> Places { get; set; }
    }
}
