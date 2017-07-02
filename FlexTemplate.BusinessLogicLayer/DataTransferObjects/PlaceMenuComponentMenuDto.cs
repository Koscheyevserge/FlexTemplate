using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class PlaceMenuComponentMenuDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PlaceMenuComponentProductDto> Products { get; set; }
    }
}
