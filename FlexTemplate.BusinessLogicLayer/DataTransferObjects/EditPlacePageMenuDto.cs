using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class EditPlacePageMenuDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EditPlacePageProductDto> Products { get; set; }
    }
}
