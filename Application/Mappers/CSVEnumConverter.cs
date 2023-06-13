using Application.Entities;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using AutoMapper;

namespace Application.Mappers
{
    public class CSVEnumConverter<TDestination> : EnumConverter where TDestination : Enum
    {
        private readonly IMapper _mapper;

        public CSVEnumConverter(Type type, IMapper mapper) : base(type)
        {
            _mapper = mapper;
        }

        public override object ConvertFromString(string source, IReaderRow row, MemberMapData memberMapData)
        {
            return _mapper.Map<TDestination>(source);
        }
    }
}
