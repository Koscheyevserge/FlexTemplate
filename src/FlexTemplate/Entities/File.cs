namespace FlexTemplate.Entities
{
    /// <summary>
    /// Файл
    /// </summary>
    public class File : BaseEntity
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Имя родительского каталога
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// Путь к файлу в локальной файловой системе
        /// </summary>
        public string Path { get; set; }
    }
}
