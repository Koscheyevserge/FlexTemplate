using System;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class EditPlacePageProductDto
    {
        public int Id { get; set; }
        public bool HasPhoto { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
