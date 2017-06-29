using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FlexTemplate.DataAccessLayer.Entities;

namespace FlexTemplate.BusinessLogicLayer.Services
{
    public static class CommonServices
    {
        public static async Task<int> GetPlacesPerPageCountAsync()
        {
            return 12;//TODO реализовать как системную настройку
        }

        public static async Task<int> GetBlogsPerPageCountAsync()
        {
            return 12;//TODO реализовать как системную настройку
        }
    }
}
