using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OS.Application.Common.Mappings
{
    public interface IMapWith<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
