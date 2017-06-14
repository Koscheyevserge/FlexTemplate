using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class CityChecklistItemDao
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public IEnumerable<int> CitiesWithoutThisIds { get; set; }
    }
}
