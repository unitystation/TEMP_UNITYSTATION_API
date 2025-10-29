using Newtonsoft.Json;
using TEMP_UNITYSTATION_API.Models;

namespace TEMP_UNITYSTATION_API.Services;

public class ServerListSingleton
{
    //
    public Dictionary<string,ServerStatus> AssignedUserListing = new Dictionary<string,ServerStatus>();
    public ActiveServers ActiveServers = new ActiveServers();
    private string activeServersData;
    public string ActiveServersData
    {
        get
        {
            if (string.IsNullOrEmpty(activeServersData) || (DateTime.UtcNow- ActiveServers.CashDateTime).TotalSeconds > 20)
            {
                SortAndGenerate();
            }
            
            return activeServersData;
        }
    }

    private List<ServerStatus> Toremove = new List<ServerStatus>();
    private List<string> ToremoveTokens = new List<string>();

    public void SortAndGenerate()
    {
        SortServers();
        activeServersData = JsonConvert.SerializeObject(ActiveServers, Formatting.Indented,
            new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
    }
    
    public void SortServers()
    {
        foreach (var server in ActiveServers.servers)
        {
            if ((DateTime.UtcNow - server.StatusOn).TotalSeconds >= 70)
            {
                Toremove.Add(server);
            }
        }

        foreach (var Removing in Toremove)
        {
            if (ActiveServers.servers.Contains(Removing))
            {
                ActiveServers.servers.Remove(Removing);
            }
        }

        foreach (var kvp in AssignedUserListing)
        {
            if (Toremove.Contains(kvp.Value))
            {
                ToremoveTokens.Add(kvp.Key);
            }
        }

        foreach (var Token in ToremoveTokens)
        {
            if (AssignedUserListing.ContainsKey(Token))
            {
                AssignedUserListing.Remove(Token);
            }   
        }
        
        Toremove.Clear();
        ToremoveTokens.Clear();
        ActiveServers.CashDateTime = DateTime.UtcNow;
        
    }
}