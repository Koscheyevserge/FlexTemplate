using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Controllers;
using FlexTemplate.Entities;

namespace FlexTemplate.ViewModels.AdminController
{
    public class AdminPagesViewModel : BaseAdminViewModel
    {
        public IEnumerable<Language> Languages { get; set; }
        public IEnumerable<Page> Pages { get; set; }
        public IEnumerable<Container> Containers { get; set; } 
    }
}
