using System.Collections.Generic;
using FlexTemplate.BusinessLogicLayer.Enums;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlacesSorting
{
    public class ViewModel
    {
        public ListTypeEnum ListType { get; set; }
        public IEnumerable<int> Categories { get; set; }
        public IEnumerable<int> Cities { get; set; }
        public PlaceOrderByEnum OrderBy { get; set; }
        public string Input { get; set; }
        public int CurrentPage { get; set; }
        public bool IsDescending { get; set; }
    }
}
