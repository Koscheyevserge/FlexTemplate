using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;

namespace FlexTemplate.ViewModels
{
    public class HeaderViewModel
    {
        public IEnumerable<Language> Languages { get; set; } 
        public Language CurrentLanguage { get; set; }
    }
}
