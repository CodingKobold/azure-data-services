using CodingKobold.DataStores.Api.Entities;
using CodingKobold.DataStores.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodingKobold.DataStores.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleEntitiesController : ControllerBase
    {
        private readonly ISampleEntitiesService _sampleEntitiesService;

        public SampleEntitiesController(ISampleEntitiesService sampleEntitiesService)
        {
            _sampleEntitiesService = sampleEntitiesService;
        }

        [HttpGet("")]
        public IEnumerable<SampleEntity> Get(int? key)
        {
            return _sampleEntitiesService.Get(key);
        }

        [HttpPost("")]
        public bool AddOrUpdate(int key, string? value)
        {
            return _sampleEntitiesService.AddOrUpdate(key, value);
        }

        [HttpDelete("")]
        public bool Delete(int key)
        {
            return _sampleEntitiesService.Delete(key);
        }

        [HttpPost("batch")]
        public bool AddBatch(Dictionary<int, string> keyValues)
        {
            return _sampleEntitiesService.AddBatch(keyValues);
        }
    }
}
