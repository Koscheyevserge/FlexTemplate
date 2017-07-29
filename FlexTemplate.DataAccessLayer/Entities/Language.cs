using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Язык локализации
    /// </summary>
    public class Language : IEntity
    {
        public int Id { get; set; }
        [NotMapped]
        public CultureInfo Culture
        {
            get
            {
                return new CultureInfo(TwoLetterISOLanguageName);
            }
            set
            {
                if (value.Parent != null && value.Parent.TwoLetterISOLanguageName != "iv")
                {
                    value = value.Parent;
                }
                TwoLetterISOLanguageName = value.TwoLetterISOLanguageName;
                Name = char.ToUpper(value.NativeName[0]) + value.NativeName.Substring(1);
            }
        }
        public string Name { get; set; }
        public string TwoLetterISOLanguageName { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
    }
}
