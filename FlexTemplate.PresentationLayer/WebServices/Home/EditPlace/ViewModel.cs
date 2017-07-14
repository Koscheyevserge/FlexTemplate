using System;
using System.Collections;
using System.Collections.Generic;

namespace FlexTemplate.PresentationLayer.WebServices.Home.EditPlace
{
    public class ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[][] Features { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MondayFrom { get; set; }
        public string MondayTo { get; set; }
        public string TuesdayFrom { get; set; }
        public string TuesdayTo { get; set; }
        public string WednesdayFrom { get; set; }
        public string WednesdayTo { get; set; }
        public string ThursdayFrom { get; set; }
        public string ThursdayTo { get; set; }
        public string FridayFrom { get; set; }
        public string FridayTo { get; set; }
        public string SaturdayFrom { get; set; }
        public string SaturdayTo { get; set; }
        public string SundayFrom { get; set; }
        public string SundayTo { get; set; }
        public List<MenuViewModel> Menus { get; set; }
        public string BannerPhotoPath { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public bool HasNoMenus { get; set; }
    }
}
