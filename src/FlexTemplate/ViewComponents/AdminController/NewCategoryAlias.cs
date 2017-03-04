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
    public class NewCategoryAlias : ViewComponent
    {
        private readonly Context _context;
        public NewCategoryAlias(Context context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke(int id)
        {
            try
            {
                var newCategoryAlias = new CategoryAlias { CategoryId = id, Id =  0, LanguageId = _context.Languages.Select(l => l.Id).First()};
                _context.CategoryAliases.Add(newCategoryAlias);
                _context.SaveChanges();
                return View(new NewCategoryAliasViewModel {Languages = _context.Languages.AsNoTracking().AsEnumerable(), Alias = newCategoryAlias});
            }
            catch
            {
                return null;
            }
        }
    }
}
