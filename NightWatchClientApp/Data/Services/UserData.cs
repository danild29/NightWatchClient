using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.Shapes;
using NightWatchClientApp.DevsFeatures;
using NightWatchClientApp.Models;
using NightWatchClientApp.Models.DTOs;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace NightWatchClientApp.Data.Services;

public class UserData : IUserData, IDisposable
{

    public readonly static string deviceAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
    private readonly static string address = deviceAddress + "/auth/";

    public static readonly HttpClient _client = new HttpClient()
    {
        BaseAddress = new Uri(address)
    };

    private JsonSerializerOptions CaseInsensitive = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    private int delay = 1000;

    public UserData()
    {
    }

    public async Task<ErrorModel> Login(UserLoginDto user)
    {
        await Task.Delay(delay);


        string json = JsonSerializer.Serialize(user);

        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("login", data);

        string result = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var u = JsonSerializer.Deserialize<User>(result, CaseInsensitive);
            UserAppInfo.UserData = u;
            return null;
        }

        var er = JsonSerializer.Deserialize<ErrorModel>(result, CaseInsensitive);
        return er;
    }




    public async Task<ErrorModel> Register(UserRegisterDto userDto)
    {

        string json = JsonSerializer.Serialize(userDto);

        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync("register", data);

        string result = await response.Content.ReadAsStringAsync();


        if (response.IsSuccessStatusCode)
        {
            var u = JsonSerializer.Deserialize<User>(result, CaseInsensitive);
            UserAppInfo.UserData = u;
            return null;
        }

        try
        {
            var er = JsonSerializer.Deserialize<ErrorModel>(result, CaseInsensitive);
            return er;
        }
        catch (Exception)
        {
            ErrorModel er = new ErrorModel();
            er.message = result;
            return er;
        }
    }


    //public async Task GetVip(UserRegisterDto userDto)
    //{
    //    string json = JsonSerializer.Serialize(userDto);

    //    StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

    //    HttpResponseMessage response = await _client.PostAsync("register", data);

    //    string result = await response.Content.ReadAsStringAsync();
    //http://localhost:5000/auth/giverole/:userId - выдать привилегию (put)
    //    {
    //        "role" : "vip"

    //}
    //}



    public void Dispose()
    {
        _client?.Dispose();
    }
}
