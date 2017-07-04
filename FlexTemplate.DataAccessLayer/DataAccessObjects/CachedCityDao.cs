using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class CachedCityDao
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CachedCityNameDao> Names { get; set; }
    }
}
