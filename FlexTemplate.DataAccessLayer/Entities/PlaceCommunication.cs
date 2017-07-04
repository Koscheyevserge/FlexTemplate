namespace FlexTemplate.DataAccessLayer.Entities
{
    public class PlaceCommunication : BaseCommunication
    {
        public int PlaceId { get; set; }
        public Place Place { get; set; }
    }
}
