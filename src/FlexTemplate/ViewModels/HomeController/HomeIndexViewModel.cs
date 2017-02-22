using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;

namespace FlexTemplate.ViewModels
{
    /// <summary>
    /// Модель, что передается на Home/Index
    /// </summary>
    public class HomeIndexViewModel
    {
        /// <summary>
        /// Словарь локализируемых строк. Формат "название строки": "значение"
        /// </summary>
        public Dictionary<string, string> Strings { get; set; } 
        /// <summary>
        /// Словарь контейнеров. Формат "название контейнера": "название шаблона"
        /// </summary>
        public Dictionary<string, string> Containers { get; set; }
    }
}
