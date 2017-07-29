using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class BlogCategoryAlias : IAlias
    {
        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public int BlogCategoryId { get; set; }
        /// <summary>
        /// Категория
        /// </summary>
        public BlogCategory BlogCategory { get; set; }
        public string Text { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public int Id { get; set; }
    }
}
