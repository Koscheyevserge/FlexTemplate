using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FlexTemplate.ViewModels
{
    /// <summary>
    ///Модель, что передается в компонент ThisCityPlaces
    /// </summary> 
    public class ThisCityPlacesViewModel : EditableViewModel
    {
       
      [MaxLength(4)]
      public List<ThisCityPlaceViewModel> ThisCityPlaceViewModels { get; set; }
      public ThisCityPlacesViewModel()
        {
            ThisCityPlaceViewModels = new List<ThisCityPlaceViewModel>
            {
                new ThisCityPlaceViewModel
                {
                    Name = "The Smoking Pug",
                    Address = "88 Thanon Surawong, Si Phraya, Bang Pak",
                    Stars = 3.5F,
                    ReviewsCount = 27,
                    PhotoPath = "~/images/hot-item/01.jpg",
                },
                new ThisCityPlaceViewModel
                {
                    Name = "Le Normandie",
                    Address = "48, Oriental Ave, Bang Rak",
                    Stars = 3.0F,
                    ReviewsCount = 32,
                    PhotoPath = "~/images/hot-item/01.jpg",
                },
                new ThisCityPlaceViewModel
                {
                    Name = "J'AIME by Jean-Michel Lorain",
                    Address = "105 Soi Sathon 1, Thung Maha Mek, Sathon",
                    Stars = 4.5F,
                    ReviewsCount = 43,
                    PhotoPath = "~/images/hot-item/01.jpg",
                },
                new ThisCityPlaceViewModel
                {
                    Name = "DID - Dine in the Dark",
                    Address = "250 Sukhumvit Rd, Bangkok",
                    Stars = 4.0F,
                    ReviewsCount = 65,
                    PhotoPath = "~/images/hot-item/01.jpg",
                },
            };
        }



    }
}
