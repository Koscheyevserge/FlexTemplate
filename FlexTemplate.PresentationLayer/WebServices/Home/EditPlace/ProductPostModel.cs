using System;

namespace FlexTemplate.PresentationLayer.WebServices.Home.EditPlace
{
    public class ProductPostModel
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public Guid Guid { get; set; }
    }
}
