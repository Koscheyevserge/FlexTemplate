using System;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Расписание заведения
    /// </summary>
    public class PlaceSchedule : IEntity
    {
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        public TimeSpan MondayFrom { get; set; }
        public TimeSpan MondayTo { get; set; }
        public TimeSpan TuesdayFrom { get; set; }
        public TimeSpan TuesdayTo { get; set; }
        public TimeSpan WednesdayFrom { get; set; }
        public TimeSpan WednesdayTo { get; set; }
        public TimeSpan ThursdayFrom { get; set; }
        public TimeSpan ThursdayTo { get; set; }
        public TimeSpan FridayFrom { get; set; }
        public TimeSpan FridayTo { get; set; }
        public TimeSpan SaturdayFrom { get; set; }
        public TimeSpan SaturdayTo { get; set; }
        public TimeSpan SundayFrom { get; set; }
        public TimeSpan SundayTo { get; set; }
        public int Id { get; set; }
    }
}