namespace FlexTemplate.DataAccessLayer.Entities
{
    public abstract class BaseLocalizableString : BaseAlias
    {
        /// <summary>
        /// Идентификатор элемента на странице, текст которого хранит локализируемая строка
        /// </summary>
        public string Tag { get; set; }
    }
}
