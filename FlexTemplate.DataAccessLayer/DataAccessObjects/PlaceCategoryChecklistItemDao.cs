using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class PlaceCategoryChecklistItemDao
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public IEnumerable<int> CategoriesWithoutThisIds { get; set; }
    }
}
