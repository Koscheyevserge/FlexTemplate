using System;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class EditPlacePageProductDao
    {
        public int Id { get; set; }
        public bool HasPhoto { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
