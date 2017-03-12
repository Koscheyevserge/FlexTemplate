using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Controllers;
using FlexTemplate.Entities;

namespace FlexTemplate.ViewModels.AdminController
{
    public class AdminContainersViewModel : BaseAdminViewModel
    {
        public IEnumerable<Language> Languages { get; set; }
        public IEnumerable<Container> Containers { get; set; }
        public ContainerLocalizableString Localization { get; set; }
        public ContainerPhoto Photo { get; set; }
        
    }
}
