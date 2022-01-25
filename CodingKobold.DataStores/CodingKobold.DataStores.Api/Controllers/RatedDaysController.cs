using CodingKobold.DataStores.Api.Models;
using CodingKobold.DataStores.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodingKobold.DataStores.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatedDaysController : ControllerBase
    {
        private readonly IRatedDaysService _ratedDaysService;

        public RatedDaysController(IRatedDaysService ratedDaysService)
        {
            _ratedDaysService = ratedDaysService;
        }

        [HttpGet(Name = "")]
        public IEnumerable<RatedDay> Get(DateTime? dateFrom, DateTime? dateTo, RatedDayType? type)
        {
            RatedDayFilters? filters = new()
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
                Type = type
            };
            var days = _ratedDaysService.Get(filters);
            return days;
        }

        [HttpPost(Name = "")]
        public bool Add(RatedDay model)
        {
            var success = _ratedDaysService.Add(model);
            return success;
        }
    }
}
