using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;

namespace FlexTemplate.Database
{
    public static class ContextProvider
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService(typeof(Context)) as Context;
            if (context == null)
            {
                return;
            }
            if (!context.UserRoles.Any())
            {
                var userRoleInitial = new UserRole {Name = "Supervisor"};
                context.Add(userRoleInitial);
                context.SaveChanges();
                if (!context.Users.Any())
                {
                    var userInitial = new User {UserRole = userRoleInitial, Login = "Supervisor"};
                    context.Add(userInitial);
                    context.SaveChanges();
                }
            }
        }
    }
}
