using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FlexTemplate.DataAccessLayer.DataAccessObjects;
using FlexTemplate.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.DataAccessLayer.Services
{
    public class Services
    {
        private FlexContext Context { get; }
        private UserManager<User> UserManager { get; }

        public Services(FlexContext context, UserManager<User> userManager)
        {
            Context = context;
            UserManager = userManager;
        }

        public PageContainersHierarchyDao GetPageContainersHierarchy(int pageContainerTemplateId)
        {
            var containers = Context.PageContainerTemplates
                .Include(pct => pct.ContainerTemplate).ThenInclude(ct => ct.Container).Include(pct => pct.Page)
                .Where(pct => pct.ParentId == pageContainerTemplateId).OrderBy(pct => pct.Position)
                .Select(pct =>
                    new PageContainerElementDao
                    {
                        Id = pct.Id,
                        ContainerName = pct.ContainerTemplate.Container.Name,
                        ContainerTemplateName = pct.ContainerTemplate.TemplateName,
                        ParentId = pct.ParentId
                    });
            var hierarchy = new PageContainersHierarchyDao { Containers = containers };
            return hierarchy;
        }

        public PageContainersHierarchyDao GetPageContainersHierarchy(string pageName)
        {
            if (Context.Pages.Count(p => p.Name == pageName) != 1)
            {
                return null;
            }
            var containers = Context.PageContainerTemplates
                .Include(pct => pct.ContainerTemplate).ThenInclude(ct => ct.Container).Include(pct => pct.Page)
                .Where(pct => pct.Page.Name == pageName && pct.ParentId == 0).OrderBy(pct => pct.Position)
                .Select(pct =>
                    new PageContainerElementDao
                    {
                        Id = pct.Id,
                        ContainerName = pct.ContainerTemplate.Container.Name,
                        ContainerTemplateName = pct.ContainerTemplate.TemplateName,
                        ParentId = pct.ParentId
                    });
            var hierarchy = new PageContainersHierarchyDao {Containers = containers};
            return hierarchy;
        }

        public async Task<bool> CanEditVisuals(ClaimsPrincipal httpContextUser)
        {
            var user = await UserManager.GetUserAsync(httpContextUser);
            return user != null && await UserManager.IsInRoleAsync(user, "Supervisor");
        }

        public async Task<bool> IsAuthor<T>(ClaimsPrincipal httpContextUser, int placeId) where T : BaseAuthorfullEntity
        {
            var user = await UserManager.GetUserAsync(httpContextUser);
            return Context.GetSet<T>().Any(p => user != null && p.User == user && p.Id == placeId);
        }
    }
}
