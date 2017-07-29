using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class Setting : IEntity
    {
        public string Code { get; set; }
        public bool BoolValue { get; set; }
        public string StringValue { get; set; }
        public int IntValue { get; set; }
        public int Id { get; set; }
    }
}
