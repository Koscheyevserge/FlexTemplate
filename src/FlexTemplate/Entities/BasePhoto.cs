namespace FlexTemplate.Entities
{
    /// <summary>
    /// Фотография
    /// </summary>
    public abstract class BasePhoto : BaseEntity
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// Имя сущности
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Переопределенный метод ToString
        /// </summary>
        /// <returns>Возвращает путь к файлу</returns>
        public override string ToString()
        {
            return $"~/Photos/{EntityName}/{EntityId}/{Name}";
        }
    }
}
