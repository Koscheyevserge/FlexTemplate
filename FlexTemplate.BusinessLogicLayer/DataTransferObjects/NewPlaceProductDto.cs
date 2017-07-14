using System;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class NewPlaceProductDto
    {
        public double Price { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public Guid Guid { get; set; }
    }
}
