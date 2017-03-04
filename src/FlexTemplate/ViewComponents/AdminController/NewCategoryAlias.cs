using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using Microsoft.AspNetCore.Mvc;

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
                var newCategoryAlias = new CategoryAlias { CategoryId = id };
                _context.CategoryAliases.Add(newCategoryAlias);
                _context.SaveChanges();
                return View(newCategoryAlias);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
