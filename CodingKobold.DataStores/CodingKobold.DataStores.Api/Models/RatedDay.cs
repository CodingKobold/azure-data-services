namespace CodingKobold.DataStores.Api.Models
{
    public class RatedDay
    {
        public DateTime Date { get; set; }
        public DayRating Rating { get; set; }
        public RatedDayType Type { get; set; }
        public string? Note { get; set; }
    }

    public enum RatedDayType
    {
        Unknown = 0,
        Casual = 1,
        Work = 2
    }

    public enum DayRating
    {
        Unknown = 0,
        Awful = 1,
        Bad = 2,
        Average = 3,
        Good = 4,
        Perfect = 5,

        NotApplicable = 100
    }
}
