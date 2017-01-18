using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FlexTemplate.Database
{
    public static class ContextProvider
    {
        public static void Initialize(IServiceProvider serviceProvider, IConfigurationRoot configuration)
        {
            using (var context = serviceProvider.GetService(typeof (Context)) as Context)
            {
                if (context == null)
                {
                    return;
                }
            }
        }
    }
}
