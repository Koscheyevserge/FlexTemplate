using System;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public abstract class BaseLocalizableString : IAlias
    {
        /// <summary>
        /// Идентификатор элемента на странице, текст которого хранит локализируемая строка
        /// </summary>
        public string Tag { get; set; }
        public string Text { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public int Id { get; set; }
    }
}
