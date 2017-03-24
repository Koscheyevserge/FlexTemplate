using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FlexTemplate.ViewModels.HomeController
{
    public class NewPlacePostViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int[] Categories { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IFormFile[] Files { get; set; }
        public bool TermAccept { get; set; }
    }
}
