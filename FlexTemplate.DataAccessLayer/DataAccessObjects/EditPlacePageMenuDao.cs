using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class EditPlacePageMenuDao
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EditPlacePageProductDao> Products { get; set; }
    }
}
