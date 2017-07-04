namespace FlexTemplate.DataAccessLayer.Entities
{
    public class ContainerLocalizableString : BaseLocalizableString
    {
        /// <summary>
        /// Идентификатор вьюкомпонента
        /// </summary>
        public int ContainerId { get; set; }
        /// <summary>
        /// Вьюкомпонент
        /// </summary>
        public Container Container { get; set; }
    }
}
