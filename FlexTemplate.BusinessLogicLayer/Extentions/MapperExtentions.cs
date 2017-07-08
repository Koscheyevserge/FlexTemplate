using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Html;
using AutoMapper;

namespace FlexTemplate.BusinessLogicLayer.Extentions
{
    public static class MapperExtentions
    {
        private static bool Initialized { get; set; }

        public static T To<T>(this object item) where T : class
        {
            if (!Initialized)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.RecognizePostfixes("Dto", "Dao", "ViewModel", "VM", "PostModel");
                    cfg.CreateMap<string, HtmlString>().ConvertUsing(s => new HtmlString(s));
                    cfg.CreateMap<string, double>().ConvertUsing(s => double.Parse(s, NumberStyles.Number));
                    cfg.CreateMissingTypeMaps = true;
                });
                Initialized = true;
            }
            return Mapper.Map<T>(item);
        }
    }
}
