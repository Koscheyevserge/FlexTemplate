using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class PlaceMenuComponentDto
    {
        public IEnumerable<PlaceMenuComponentMenuDto> Menus { get; set; }
    }
}
