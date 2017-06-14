using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using FlexTemplate.PresentationLayer.WebServices.Home.Places;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlacesPagination
{
    public class PlacesPagination : FlexViewComponent
    {
        private ComponentsServices ComponentsServices { get; }

        public PlacesPagination(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync([FromQuery] int currentPage, [FromQuery] string input, [FromQuery] int orderBy, [FromQuery] int[] cities, [FromQuery] int[] categories, [FromQuery] bool isDescending, [FromQuery] int listType, int placesCount)
        {
            var serviceResult = await ComponentsServices.GetPlacesPaginationAsync(placesCount, currentPage);
            var paginationVM = serviceResult.To<PaginationViewModel>();
            var model = new ViewModel
            {
                OrderBy = orderBy,
                Cities = cities,
                Categories = categories,
                ListType = listType,
                IsDescending = isDescending,
                Input = input,
                CurrentPage = currentPage,
                HasNextPage = paginationVM.HasNextPage,
                HasPreviousPage = paginationVM.HasPreviousPage,
                Pages = paginationVM.TotalPagesCount
            };
            return View(model);
        }
    }
}
