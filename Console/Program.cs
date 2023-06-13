using System.Threading.Tasks;
using Application;

namespace Console;

internal class Program
{
    const string ConfigFilePath = "config.json";
    private static async Task Main()
    {
        ServerConfig config = null;
        try
        {
            config = await ServerConfig.FromFile(ConfigFilePath);
        }
        catch
        {
            System.Console.WriteLine("Error. Inncorect cofig file.");
        }

        if (config != null) {
            var server = new Server(config);
            while (true)
            {
                var unparsedOrder = System.Console.ReadLine();
                var output = await server.TakeOrderAsync(unparsedOrder);
                System.Console.WriteLine(output);
            }
        }
    }
}