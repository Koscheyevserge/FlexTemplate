using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class NewPlaceMenuDto
    {
        public string Name { get; set; }
        public IEnumerable<NewPlaceProductDto> Products { get; set; }
    }
}
