using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class BlogCategoryAlias : BaseAlias
    {
        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public int BlogCategoryId { get; set; }
        /// <summary>
        /// Категория
        /// </summary>
        public BlogCategory BlogCategory { get; set; }
    }
}
