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
                    Name = "Дрим Таун",
                    Address = "Оболонский проспект",
                    Stars = 3.5,
                    ReviewsCount = 27,
                    PhotoPath = "images/hot-item/01.jpg",
                    Categories = new List<string>
                    {
                        "ТРЦ",
                        "Кинотеатр",
                        "Каток",
                        "Рестораны"
                    }
                },
                new ThisCityPlaceViewModel
                {
                    Name = "Дрим Таун",
                    Address = "Оболонский проспект",
                    Stars = 3.5,
                    ReviewsCount = 27,
                    PhotoPath = "images/hot-item/01.jpg",
                    Categories = new List<string>
                    {
                        "ТРЦ",
                        "Кинотеатр",
                        "Каток",
                        "Рестораны"
                    }
                },
                new ThisCityPlaceViewModel
                {
                    Name = "Дрим Таун",
                    Address = "Оболонский проспект",
                    Stars = 3.5,
                    ReviewsCount = 27,
                    PhotoPath = "images/hot-item/01.jpg",
                    Categories = new List<string>
                    {
                        "ТРЦ",
                        "Кинотеатр",
                        "Каток",
                        "Рестораны"
                    }
                },
                new ThisCityPlaceViewModel
                {
                    Name = "Дрим Таун",
                    Address = "Оболонский проспект",
                    Stars = 3.5,
                    ReviewsCount = 27,
                    PhotoPath = "images/hot-item/01.jpg",
                    Categories = new List<string>
                    {
                        "ТРЦ",
                        "Кинотеатр",
                        "Каток",
                        "Рестораны"
                    }
                },
                                new ThisCityPlaceViewModel
                {
                    Name = "Дрим Таун",
                    Address = "Оболонский проспект",
                    Stars = 3.5,
                    ReviewsCount = 27,
                    PhotoPath = "images/hot-item/01.jpg",
                    Categories = new List<string>
                    {
                        "ТРЦ",
                        "Кинотеатр",
                        "Каток",
                        "Рестораны"
                    }
                },
                new ThisCityPlaceViewModel
                {
                    Name = "Дрим Таун",
                    Address = "Оболонский проспект",
                    Stars = 3.5,
                    ReviewsCount = 27,
                    PhotoPath = "images/hot-item/01.jpg",
                    Categories = new List<string>
                    {
                        "ТРЦ",
                        "Кинотеатр",
                        "Каток",
                        "Рестораны"
                    }
                },
                new ThisCityPlaceViewModel
                {
                    Name = "Дрим Таун",
                    Address = "Оболонский проспект",
                    Stars = 3.5,
                    ReviewsCount = 27,
                    PhotoPath = "images/hot-item/01.jpg",
                    Categories = new List<string>
                    {
                        "ТРЦ",
                        "Кинотеатр",
                        "Каток",
                        "Рестораны"
                    }
                },
                new ThisCityPlaceViewModel
                {
                    Name = "Дрим Таун",
                    Address = "Оболонский проспект",
                    Stars = 3.5,
                    ReviewsCount = 27,
                    PhotoPath = "images/hot-item/01.jpg",
                    Categories = new List<string>
                    {
                        "ТРЦ",
                        "Кинотеатр",
                        "Каток",
                        "Рестораны"
                    }
                }
            };
        }

    }
}
