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
        
        [Route("api/resources/photo-detail/{id}")]
        public async Task<IEnumerable<string>> PhotoDetail(int id)
        {
            var result = await BllServices.GetPhotoDetailAsync(id);
            return result;
        }

        [Route("api/resources/cities")]
        public async Task<IEnumerable<string>> GetCities()
        {
            var result = await BllServices.GetCitiesAsync();
            return result;
        }

        [Route("api/resources/tags")]
        public async Task<IEnumerable<string>> GetTags()
        {
            var result = await BllServices.GetTagsAsync();
            return result;
        }
    }
}