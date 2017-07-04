using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class PlaceCategoryChecklistItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public IEnumerable<int> CategoriesWithoutThisIds { get; set; }
    }
}
