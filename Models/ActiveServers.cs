namespace TEMP_UNITYSTATION_API.Models;

[Serializable]
public class ActiveServers
{
    public DateTime CashDateTime = DateTime.MinValue;
    public List<ServerStatus> servers = new List<ServerStatus>();

}

[Serializable]
public class ServerStatus
{
    public DateTime StatusOn;
    public bool Passworded;
    public string ServerName;
    public string ForkName;
    public int BuildVersion;
    public string CurrentMap;
    public string GameMode;
    public string IngameTime;
    public string RoundTime;
    public int PlayerCount;
    public int PlayerCountMax;
    public string ServerIP;
    public int ServerPort;
    public string WinDownload;
    public string OSXDownload;
    public string LinuxDownload;
    public int fps;
    public string SerializedMetaJson;
    public string GoodFileVersion;
}
