using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public interface ILanguagefull : IEntity
    {
        int LanguageId { get; set; }
        Language Language { get; set; }
    }
}
