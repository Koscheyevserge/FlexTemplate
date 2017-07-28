using System;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class NewPlaceProductComponentDto
    {
        public string Name { get; set; }
        public int Menu { get; set; }
        public int Position { get; set; }
        public Guid Guid { get; set; }
    }
}
