using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TEMP_UNITYSTATION_API.Models;
using TEMP_UNITYSTATION_API.Services;
using JsonSerializerOptions = System.Text.Json.JsonSerializerOptions;

namespace TEMP_UNITYSTATION_API.Controllers;

[ApiController]
[Route("statusupdate")]
public class statusupdateController : Controller
{
    private readonly ServerListSingleton _ServerListSingleton;
    
    private readonly BabyAuthenticationSingleton _BabyAuthenticationSingleton;
    public statusupdateController(ServerListSingleton ServerListSingleton, BabyAuthenticationSingleton BabyAuthenticationSingleton)
    {
        _ServerListSingleton = ServerListSingleton;
        _BabyAuthenticationSingleton = BabyAuthenticationSingleton;
        
    }
    
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string data, [FromQuery] string user )
    {
     
        //await Task.Delay(2000);
        ServerStatus ServerStatus = null;
        
        if (HttpContext.Session.GetString(user.ToLower()) == null)
        {
            return BadRequest(new ApiResponse()
            {
                errorCode = 600,
                errorMsg = "Error 600: Access Denied"
            });
        }
        
        try
        {
            ServerStatus = JsonConvert.DeserializeObject<ServerStatus>(data);
        }
        catch (Exception e)
        {
            return BadRequest("Your model is incorrect Bitch!!");
        }
        
        if (ServerStatus == null)
        {
            return BadRequest("Your model is incorrect Bitch!!");
        }

        ServerStatus.StatusOn = DateTime.UtcNow;

        ServerStatus OldServerStatus = null;
        
        if (_ServerListSingleton.AssignedUserListing.ContainsKey(user))
        {
            OldServerStatus = _ServerListSingleton.AssignedUserListing[user];
        }
        
        _ServerListSingleton.AssignedUserListing[user] = ServerStatus;

        if (OldServerStatus != null)
        {
            if (_ServerListSingleton.ActiveServers.servers.Contains(OldServerStatus))
            {
                _ServerListSingleton.ActiveServers.servers.Remove(OldServerStatus);
            }
        }

        _ServerListSingleton.ActiveServers.servers.Add(ServerStatus);

        _ServerListSingleton.ActiveServers.servers = _ServerListSingleton.ActiveServers.servers.OrderByDescending(o => o.PlayerCount).ToList();
        _ServerListSingleton.SortAndGenerate();
        HttpContext.Response.Cookies.Delete(".AspNetCore.Session");
        return Ok(new ApiResponse()
        {
            message = "Server status update success!"
        });
    }
}