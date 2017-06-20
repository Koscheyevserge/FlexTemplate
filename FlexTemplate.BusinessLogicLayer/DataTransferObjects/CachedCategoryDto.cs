using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class CachedCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CachedCategoryNameDto> Names { get; set; }
    }
}
