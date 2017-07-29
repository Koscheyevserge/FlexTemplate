namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Алиас
    /// </summary>
    public interface IAlias : IEntity
    {
        /// <summary>
        /// Текст локализируемой строки
        /// </summary>
        string Text { get; set; }
        /// <summary>
        /// Идентификатор языка локализируемой строки
        /// </summary>
        int LanguageId { get; set; }
        /// <summary>
        /// Язык локализируемой строки
        /// </summary>
        Language Language { get; set; }
    }
}
