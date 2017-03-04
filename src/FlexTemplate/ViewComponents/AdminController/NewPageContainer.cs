using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using FlexTemplate.ViewModels.AdminController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.ViewComponents.AdminController
{
    public class NewPageContainer : ViewComponent
    {
        private readonly Context _context;
        public NewPageContainer(Context context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke(int id)
        {
            try
            {
                var model = new PageContainerTemplate{ PageId = id, ContainerTemplateId = _context.ContainerTemplates.Select(ct => ct.Id).First()};
                _context.PageContainerTemplates.Add(model);
                _context.SaveChanges();
                return View(new NewPageContainerViewModel { Languages = _context.Languages.AsNoTracking().AsEnumerable(), Containers = _context.Containers.AsNoTracking().AsEnumerable(), PageContainerTemplate = model});
            }
            catch
            {
                return null;
            }
        }
    }
}
