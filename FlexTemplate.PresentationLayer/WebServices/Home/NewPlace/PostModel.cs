using System;
using System.Collections.Generic;

namespace FlexTemplate.PresentationLayer.WebServices.Home.NewPlace
{
    public class PostModel
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public TimeSpan MondayFrom { get; set; }
        public TimeSpan MondayTo { get; set; }
        public TimeSpan TuesdayFrom { get; set; }
        public TimeSpan TuesdayTo { get; set; }
        public TimeSpan WednesdayFrom { get; set; }
        public TimeSpan WednesdayTo { get; set; }
        public TimeSpan ThursdayFrom { get; set; }
        public TimeSpan ThursdayTo { get; set; }
        public TimeSpan FridayFrom { get; set; }
        public TimeSpan FridayTo { get; set; }
        public TimeSpan SaturdayFrom { get; set; }
        public TimeSpan SaturdayTo { get; set; }
        public TimeSpan SundayFrom { get; set; }
        public TimeSpan SundayTo { get; set; }
        public string[][] Features { get; set; }
        public IEnumerable<MenuPostModel> Menus { get; set; }
        public IEnumerable<int> Categories { get; set; }
        public Guid BlobKey { get; set; }
    }
}
