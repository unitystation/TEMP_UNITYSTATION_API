using System.Reflection;
using Newtonsoft.Json;
using TEMP_UNITYSTATION_API.Models;

namespace TEMP_UNITYSTATION_API.Services;

public class BabyAuthenticationLoad(BabyAuthenticationSingleton dataService) : IHostedService
{

    private BabyAuthenticationSingleton BabyAuthenticationSingleton = dataService;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var _filePath = Path.Combine(
            Assembly.GetExecutingAssembly().Location.Replace("TEMP_UNITYSTATION_API.dll", ""), 
            "tokens", "allowed.json");
        Console.WriteLine($"Reading file from: {_filePath}");

        if (File.Exists(_filePath))
        {
            string jsonContent = File.ReadAllText(_filePath);
            Console.WriteLine($"Reading file data {jsonContent}");
            BabyAuthenticationSingleton.GoodBoyTokens = JsonConvert.DeserializeObject<HashSet<HubLogin>>(jsonContent);
        }
        else
        {
            Console.WriteLine("File not found!");
        }
        return Task.CompletedTask;
       
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}