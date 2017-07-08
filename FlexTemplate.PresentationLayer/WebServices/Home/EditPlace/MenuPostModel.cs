using System.Collections.Generic;

namespace FlexTemplate.PresentationLayer.WebServices.Home.EditPlace
{
    public class MenuPostModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProductPostModel> Products { get; set; }
    }
}
