namespace TEMP_UNITYSTATION_API.Models;

[Serializable]
public class ApiResponse
{
    public int errorCode = 0; //0 = all good, read the message variable now, otherwise read errorMsg
    public string errorMsg;
    public string message;
}