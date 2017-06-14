using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlexTemplate.BusinessLogicLayer.Services
{
    public static class CommonServices
    {
        public static async Task<int> GetPlacesPerPageCountAsync()
        {
            return 12;//TODO реализовать как системную настройку
        }
    }
}
