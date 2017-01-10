namespace FlexTemplate.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Локализируемые строки
    /// </summary>
    public class LocalizableString : BaseEntity
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
