using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    /// <summary>
    /// Алиас категории
    /// </summary>
    public class CategoryAlias : BaseAlias
    {
        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// Категория
        /// </summary>
        public Category Category { get; set; }
    }
}
