using System;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class EditPlaceProductDao
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public Guid Guid { get; set; }
    }
}
