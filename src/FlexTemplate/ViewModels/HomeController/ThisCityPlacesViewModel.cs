using System.Collections.Generic;

namespace FlexTemplate.ViewModels.HomeController
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
