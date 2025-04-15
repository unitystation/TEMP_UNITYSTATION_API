using TEMP_UNITYSTATION_API.Models;

namespace TEMP_UNITYSTATION_API.Services;

public class BabyAuthenticationSingleton
{
    public HashSet<HubLogin>? GoodBoyTokens = new HashSet<HubLogin>();
}