using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.ViewModels
{
    /// <summary>
    ///Модель, что передается в компонент ThisCityPlaces
    /// </summary> 
    public class ThisCityPlacesViewModel : EditableViewModel
    {
        public List<int> ThisCityPlaceIds { get; set; }
        public Dictionary<string, string> Strings { get; set; }
    }
}
