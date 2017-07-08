using System;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class EditPlaceProductDto
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public Guid Guid { get; set; }
    }
}
