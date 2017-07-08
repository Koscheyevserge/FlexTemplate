using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class EditPlaceMenuDao
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<EditPlaceProductDao> Products { get; set; }
    }
}
