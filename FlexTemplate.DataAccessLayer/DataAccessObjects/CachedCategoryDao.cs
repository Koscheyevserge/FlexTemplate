using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class CachedCategoryDao
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CachedCategoryNameDao> Names { get; set; }
    }
}
