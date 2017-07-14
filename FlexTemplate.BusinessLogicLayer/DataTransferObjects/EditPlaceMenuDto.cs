using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class EditPlaceMenuDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<EditPlaceProductDto> Products { get; set; }
    }
}
