using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public abstract class BaseCachedNameDto
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }
    }
}
