namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class PlaceOverviewComponentDao
    {
        public string Description { get; set; }
        public bool HasSchedule { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public PlaceOverviewComponentScheduleDao Schedule { get; set; }
        public string PlaceCategoriesEnumerated { get; set; }
        public string[] RowsOfFeatures { get; set; }
    }
}
