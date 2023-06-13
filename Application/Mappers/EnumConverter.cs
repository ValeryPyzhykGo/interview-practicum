using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class EnumConverter<TDestination> : ITypeConverter<string, TDestination>
    where TDestination : Enum
    {
        public TDestination Convert(string source, TDestination destination, ResolutionContext context)
        {

            if (Enum.TryParse(typeof(TDestination), source, true, out object result))
            {
                return (TDestination)result;
            }

            var values = Enum.GetValues(typeof(TDestination));
            foreach (TDestination value in values)
            {
                var type = value.GetType();
                var memInfo = type.GetMember(value.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attributes.Length > 0 && ((DisplayAttribute)attributes[0]).GetName().ToLower() == source)
                {
                    return value;
                }
            }

            throw new NotSupportedException($"Enum value '{source}' is not supported.");
        }
    }
}
