using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TEMP_UNITYSTATION_API.Models;
using TEMP_UNITYSTATION_API.Services;

namespace TEMP_UNITYSTATION_API.Controllers;


[ApiController]
[Route("login")]
public class AccountLoginController : Controller
{
    private readonly ServerListSingleton _ServerListSingleton;
    private readonly BabyAuthenticationSingleton _BabyAuthenticationSingleton;
    public AccountLoginController(ServerListSingleton ServerListSingleton,BabyAuthenticationSingleton  BabyAuthenticationSingleton)
    {
        _ServerListSingleton = ServerListSingleton;
        _BabyAuthenticationSingleton = BabyAuthenticationSingleton;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string data )
    {
        data = Regex.Unescape(data);
        var request = JsonConvert.DeserializeObject<HubLogin>(data);

        
        if (string.IsNullOrEmpty(request.username) || string.IsNullOrEmpty(request.password))
        {
            return BadRequest("Is empty for some reason");
        }
        
        //await Task.Delay(2000);
        request.username = request.username.ToLower();
        HttpContext.Session.Clear();
    
        if (_BabyAuthenticationSingleton.GoodBoyTokens.Any(x =>
                x.username == request.username
                && x.password == request.password))
        {
            HttpContext.Session.SetString(request.username,  DateTime.UtcNow.ToString("o"));
            return Ok(new ApiResponse()
            {
                message = "Success"
            });
        }
        else
            return BadRequest(new ApiResponse()
            {
                errorCode =  901,
                errorMsg = "Username or password is incorrect!"
            });
        {
        }

    
    }
}