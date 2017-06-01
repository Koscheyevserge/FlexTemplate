﻿using System;
using System.Collections.Generic;
using System.Text;
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
                    cfg.CreateMissingTypeMaps = true;
                });
                Initialized = true;
            }
            return Mapper.Map<T>(item);
        }
    }
}
