using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;

namespace FlexTemplate.ViewModels.HomeController
{
    public class EditPlaceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int[] Categories { get; set; }
        public string[][] Features { get; set; }
        public IEnumerable<Category> CurrentCategories { get; set; }
        public IEnumerable<Category> AllCategories { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public TimeSpan MondayFrom { get; set; }
        public TimeSpan MondayTo { get; set; }
        public TimeSpan TuesdayFrom { get; set; }
        public TimeSpan TuesdayTo { get; set; }
        public TimeSpan WednesdayFrom { get; set; }
        public TimeSpan WednesdayTo { get; set; }
        public TimeSpan ThurstdayFrom { get; set; }
        public TimeSpan ThurstdayTo { get; set; }
        public TimeSpan FridayFrom { get; set; }
        public TimeSpan FridayTo { get; set; }
        public TimeSpan SaturdayFrom { get; set; }
        public TimeSpan SaturdayTo { get; set; }
        public TimeSpan SundayFrom { get; set; }
        public TimeSpan SundayTo { get; set; }
        public List<EditPlaceMenuViewModel> Menus { get; set; }
    }

    public class EditPlaceMenuViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EditPlaceProductViewModel> Products { get; set; } 
    }

    public class EditPlaceProductViewModel
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public bool HasPhoto { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
