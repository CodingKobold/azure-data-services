namespace CodingKobold.DataStores.Api.Models
{
    public class RatedDayFilters
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public RatedDayType? Type { get; set; }
    }
}
