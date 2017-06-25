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

        public async Task<IViewComponentResult> InvokeAsync(int placesCount)
        {
            int.TryParse(Request.Query["currentPage"], out int currentPage);
            var serviceResult = await ComponentsServices.GetPlacesPaginationAsync(placesCount, currentPage);
            var input = Request.Query["input"].ToString();
            int.TryParse(Request.Query["orderBy"], out int orderBy);
            var cities = Request.Query["cities"].ToArray().Select(int.Parse);
            var categories = Request.Query["categories"].ToArray().Select(int.Parse);
            bool.TryParse(Request.Query["isDescending"], out bool isDescending);
            int.TryParse(Request.Query["listType"], out int listType);
            var paginationVM = serviceResult.To<PaginationViewModel>();
            var model = new ViewModel
            {
                PlacesCount = placesCount,
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
