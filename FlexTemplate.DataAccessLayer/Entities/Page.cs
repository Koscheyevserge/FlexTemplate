using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Расширения для страницы
    /// </summary>
    public class Page : BaseEntity
    {
        /// <summary>
        /// Название страницы
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Титул страницы по-умолчанию
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Контейнеры на странице
        /// </summary>
        public virtual List<PageContainerTemplate> Containers { get; set; }
    }
}
