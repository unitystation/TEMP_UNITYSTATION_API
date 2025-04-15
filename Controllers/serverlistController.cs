using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TEMP_UNITYSTATION_API.Services;

namespace TEMP_UNITYSTATION_API.Controllers;

[ApiController]
[Route("serverlist")]
public class serverlistController : Controller
{
    private readonly ServerListSingleton _ServerListSingleton;
    
    public serverlistController(ServerListSingleton ServerListSingleton)
    {
        _ServerListSingleton = ServerListSingleton;

    }
    
    
    [HttpGet]
    public IActionResult Get()
    {
        return new ContentResult
        {
            Content = _ServerListSingleton.ActiveServersData,
            ContentType = "application/json", 
            StatusCode = 200  
        };
    }
}