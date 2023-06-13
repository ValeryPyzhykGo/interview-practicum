using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application
{
    public class ServerConfig
    {
        public string MenuFilePath { get; set; }

        public static async Task<ServerConfig> FromFile(string filePath)
        {

            using (FileStream fileStream = File.OpenRead(filePath))
            {
                return await JsonSerializer.DeserializeAsync<ServerConfig>(fileStream);  
            }
        }
    }
}
