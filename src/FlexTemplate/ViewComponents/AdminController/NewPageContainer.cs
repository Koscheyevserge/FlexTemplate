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
                var entity = new PageContainerTemplate{ ContainerTemplate = _context.ContainerTemplates.Include(ct => ct.Container).ThenInclude(c => c.AvailableContainers).FirstOrDefault(ct => ct.Container.AvailableContainers.Any(ac => ac.PageId == id)), PageId = id};
                if (entity.ContainerTemplate == null)
                    return null;
                var model = new NewPageContainerViewModel
                {
                    Containers = _context.Containers.Include(c => c.AvailableContainers).AsNoTracking().AsEnumerable(),
                    PageContainerTemplate = entity
                };
                return View(model);
            }
            catch
            {
                return null;
            }
        }
    }
}