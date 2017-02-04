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
       
     
      public List<ThisCityPlaceViewModel> ThisCityPlaceViewModels { get; set; }
      public ThisCityPlacesViewModel()
        {
            ThisCityPlaceViewModels = new List<ThisCityPlaceViewModel>
            {
                new ThisCityPlaceViewModel
                {
                    Name = "Фламінго",
                    Address = "Вул. Симоненка 1",
                    Stars = 3,
                    ReviewsCount = 27,
                    PhotoPath = "images/hot-item/01.jpg",
                    Categories = new List<string>
                    {
                        "Українська",
                        "Грузинська"
                    }
                },
                new ThisCityPlaceViewModel
                {
                    Name = "Пузата хата",
                    Address = "Оболонський проспект",
                    Stars = 4,
                    ReviewsCount = 27,
                    PhotoPath = "images/hot-item/01.jpg",
                    Categories = new List<string>
                    {
                        "Українська"
                    }
                },
                new ThisCityPlaceViewModel
                {
                    Name = "Мафія",
                    Address = "Площа Ринок",
                    Stars = 3,
                    ReviewsCount = 27,
                    PhotoPath = "images/hot-item/01.jpg",
                    Categories = new List<string>
                    {
                        "Японська",
                        "Італійська"
                    }
                },
                new ThisCityPlaceViewModel
                {
                    Name = "М’ясо",
                    Address = "Вул. Шота Руставелі",
                    Stars = 5,
                    ReviewsCount = 27,
                    PhotoPath = "images/hot-item/01.jpg",
                    Categories = new List<string>
                    {
                        "Американська"
                    }
                },
                new ThisCityPlaceViewModel
                {
                    Name = "Кафе Мазох",
                    Address = "Катедральна площа",
                    Stars = 4,
                    ReviewsCount = 27,
                    PhotoPath = "images/hot-item/01.jpg",
                    Categories = new List<string>
                    {
                        "Українська"
                    }
                },
                new ThisCityPlaceViewModel
                {
                    Name = "Криївка",
                    Address = "Вул. Незалежності",
                    Stars = 5,
                    ReviewsCount = 27,
                    PhotoPath = "images/hot-item/01.jpg",
                    Categories = new List<string>
                    {
                        "Українська"
                    }
                },
                new ThisCityPlaceViewModel
                {
                    Name = "La petite",
                    Address = "Проспект Сім’ї Соснових",
                    Stars = 5,
                    ReviewsCount = 27,
                    PhotoPath = "images/hot-item/01.jpg",
                    Categories = new List<string>
                    {
                        "Французька"
                    }
                },
                new ThisCityPlaceViewModel
                {
                    Name = "Pizza+",
                    Address = "Вул. Військова",
                    Stars = 5,
                    ReviewsCount = 27,
                    PhotoPath = "images/hot-item/01.jpg",
                    Categories = new List<string>
                    {
                        "Італійська"
                    }
                }
            };
        }

    }
}
