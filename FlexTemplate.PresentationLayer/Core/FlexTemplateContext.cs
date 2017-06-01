using FlexTemplate.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.PresentationLayer.Core
{
    public class FlexTemplateContext : FlexContext
    {
        public FlexTemplateContext(DbContextOptions options) : base(options)
        {

        }

        public FlexTemplateContext()
        {
            
        }
    }
}