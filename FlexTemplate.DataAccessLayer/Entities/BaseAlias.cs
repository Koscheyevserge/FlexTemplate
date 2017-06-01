namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Локализируемые строки
    /// </summary>
    public abstract class BaseAlias : BaseEntity
    {
        /// <summary>
        /// Текст локализируемой строки
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Идентификатор языка локализируемой строки
        /// </summary>
        public int LanguageId { get; set; }
        /// <summary>
        /// Язык локализируемой строки
        /// </summary>
        public virtual Language Language { get; set; }
    }
}
