using Application.Entities;
using Application.Mappers;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    internal class MenuRepository : IMenuRepository
    {
        private readonly ServerConfig _config;
        private readonly CSVEnumConverter<Daytime> _daytimeConverte;
        private readonly CSVEnumConverter<DishType> _dishTypeConvertor;

        public MenuRepository(ServerConfig config, CSVEnumConverter<Daytime> dayTimeConverte, CSVEnumConverter<DishType> dishTypeConvertor)
        {
            _config = config;
            _daytimeConverte = dayTimeConverte;
            _dishTypeConvertor = dishTypeConvertor;
        }

        public async Task<List<MenuPosition>> ReadMenuAsync()
        {
            return await Task.Run(() =>
            {
                using (var reader = new StreamReader(_config.MenuFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var result = new List<MenuPosition>();
                    csv.Context.TypeConverterCache.AddConverter<Daytime>(_daytimeConverte);
                    csv.Context.TypeConverterCache.AddConverter<DishType>(_dishTypeConvertor);
                    while (csv.Read())
                    {
                        result.Add(csv.GetRecord<MenuPosition>());
                    }
                    return result;
                }
            });
        }
    }
}
