using Azure.Data.Tables;
using CodingKobold.DataStores.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodingKobold.DataStores.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicEntitiesController : ControllerBase
    {
        private readonly IDynamicEntitiesService _dynamicEntitiesService;

        public DynamicEntitiesController(IDynamicEntitiesService dynamicEntitiesService)
        {
            _dynamicEntitiesService = dynamicEntitiesService;
        }

        [HttpGet("")]
        public IEnumerable<TableEntity> Get(int? key)
        {
            return _dynamicEntitiesService.Get(key);
        }

        [HttpPost("")]
        public bool AddOrUpdate(int key, Dictionary<string, string> keyValues)
        {
            return _dynamicEntitiesService.AddOrUpdate(key, keyValues);
        }

        [HttpDelete("")]
        public bool Delete(int key)
        {
            return _dynamicEntitiesService.Delete(key);
        }
    }
}
