namespace FlexTemplate.DataAccessLayer.Entities
{
    public abstract class BaseCommunication : BaseEntity
    {
        public string Number { get; set; }
        public int CommunicationTypeId { get; set; }
        public CommunicationType CommunicationType { get; set; }
    }
}
