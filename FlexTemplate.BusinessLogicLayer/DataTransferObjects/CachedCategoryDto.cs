using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class CachedCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CachedCategoryNameDto> Names { get; set; }
    }
}
