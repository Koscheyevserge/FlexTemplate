﻿using System.Collections.Generic;

namespace FlexTemplate.ViewModels.HomeController
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
        public IEnumerable<KeyValuePair<string, string>> Containers { get; set; }
    }
}
