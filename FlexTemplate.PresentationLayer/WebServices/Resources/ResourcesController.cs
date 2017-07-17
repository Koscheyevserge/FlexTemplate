using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Resources
{
    public class ResourcesController : FlexController
    {
        public ResourcesController(ControllerServices services) : base(services)
        {
        }
        
        /*[Route("api/resources/photo-detail/{id}")]
        public IEnumerable<string> PhotoDetail(int id)
        {
            var path = $@"wwwroot\Resources\Places\{id}\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var result = Directory.GetFiles(path).Except(new [] { $@"wwwroot\Resources\Places\{id}\head.jpg" , $@"wwwroot\Resources\Places\{id}\banner.jpg" });
            return result;
        }*/

        [Route("api/resources/cities")]
        public async Task<IEnumerable<string>> GetCities()
        {
            return await BllServices.GetCitiesAsync();
        }

        [Route("api/resources/tags")]
        public async Task<IEnumerable<string>> GetTags()
        {
            return await BllServices.GetTagsAsync();
        }
    }
}