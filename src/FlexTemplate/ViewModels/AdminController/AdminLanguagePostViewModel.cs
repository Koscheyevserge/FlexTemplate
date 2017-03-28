using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.ViewModels.AdminController
{
    public class LanguagePostViewModel
    {
        public int DefaultLanguage { get; set; }
        public IEnumerable<AdminLanguagePostViewModelItem> Languages { get; set; }
    }
}
