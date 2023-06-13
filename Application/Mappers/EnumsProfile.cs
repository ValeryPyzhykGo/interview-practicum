using Application.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class EnumsProfile : Profile
    {
        public EnumsProfile()
        {
            CreateMap<string, Daytime>().ConvertUsing<EnumConverter<Daytime>>();
            CreateMap<string, DishType>().ConvertUsing<EnumConverter<DishType>>();
        }
    }
}
