using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class PlaceMenuComponentDto
    {
        public IEnumerable<PlaceMenuComponentMenuDto> Menus { get; set; }
        public bool HasMenus { get; set; }
    }
}
