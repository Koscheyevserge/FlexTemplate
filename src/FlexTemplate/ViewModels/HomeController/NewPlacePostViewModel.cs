using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FlexTemplate.ViewModels.HomeController
{
    public class NewPlacePostViewModel
    {
        [Required(ErrorMessage = "Необхідно вказати назв")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int[] Categories { get; set; }
        public string[][] Features { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Uid { get; set; }
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
        public NewPlaceMenuViewModel[] Menus { get; set; } 
    }
}
