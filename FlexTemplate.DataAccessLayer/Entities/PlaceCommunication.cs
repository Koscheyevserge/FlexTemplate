using System;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class PlaceCommunication : ICommunication
    {
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        public string Number { get; set; }
        public int CommunicationType { get; set; }
        public int Id { get; set; }
    }
}
