using System;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public interface ICommunication : IEntity
    {
        string Number { get; set; }
        int CommunicationType { get; set; }
    }
}
