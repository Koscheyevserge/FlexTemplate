using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.ViewModels
{
    /// <summary>
    /// Базовый класс для вьюмоделей, которые могут настраиваться пользователем
    /// </summary>
    public abstract class EditableViewModel
    {
        /// <summary>
        /// Может ли данный пользователь настраивать этот компонент?
        /// </summary>
        public bool CanEdit { get; set; }
        /// <summary>
        /// Видим ли этот компонент на странице?
        /// </summary>
        public bool IsVisible { get; set; }
    }
}
