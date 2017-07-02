using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class PlaceMenuComponentMenuDao
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PlaceMenuComponentProductDao> Products { get; set; }
    }
}
