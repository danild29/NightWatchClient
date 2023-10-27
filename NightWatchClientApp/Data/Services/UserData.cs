using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


namespace NightWatchClientApp.Data.Services;

public class UserData : IUserData, IDisposable
{

    //public readonly static string deviceAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
    public readonly static string deviceAddress = "http://213.171.4.235:5000";
    private readonly static string address = deviceAddress + "/auth/";

    public static readonly HttpClient _client = new HttpClient()
    {
        BaseAddress = new Uri(address)
    };

    private JsonSerializerOptions CaseInsensitive = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };



    public UserData()
    {
    }

    public async Task<InfoModel> Login(UserLoginDto user)
    {


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

        var er = JsonSerializer.Deserialize<InfoModel>(result, CaseInsensitive);
        return er;
    }

    public async Task<InfoModel> Register(UserRegisterDto userDto)
    {

        string json = JsonSerializer.Serialize(userDto);

        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync("register", data);

        string result = await response.Content.ReadAsStringAsync();


        if (response.IsSuccessStatusCode)
        {
            UserLoginDto loginDto = new(userDto.name, userDto.password);
            return await Login(loginDto);
        }

        try
        {
            var er = JsonSerializer.Deserialize<InfoModel>(result, CaseInsensitive);
            return er;
        }
        catch (Exception)
        {
            InfoModel er = new InfoModel();
            er.message = result;
            return er;
        }
    }

    public async Task<InfoModel> GetMyTeam()
    {

        HttpResponseMessage response = await _client.GetAsync($"get-team-by-id/{UserAppInfo.UserData._id}");
        string result = await response.Content.ReadAsStringAsync();

        var info = JsonSerializer.Deserialize<InfoModel>(result, CaseInsensitive);
        if (!string.IsNullOrEmpty(info?.message))
            return info;


        if (response.IsSuccessStatusCode)
        {

            var u = JsonSerializer.Deserialize<Team>(result, CaseInsensitive);
            UserAppInfo.TeamData = u;
            return null;
        }


        return null;
    }


    public async Task<InfoModel> GetVip(string userid, UserLoginDto user)
    {
        try
        {
            UserLoginDto admin = new("admin", "admin");
            string json = JsonSerializer.Serialize(admin);

            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("login", data);

            string result = await response.Content.ReadAsStringAsync();


            if (!response.IsSuccessStatusCode) return new InfoModel("Что-то пошло не так.");

            var u = JsonSerializer.Deserialize<User>(result, CaseInsensitive);


            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", u.Token);

            string j = JsonSerializer.Serialize(new { role = "vip" });


            StringContent info = new StringContent(j, Encoding.UTF8, "application/json");

            HttpResponseMessage userResponse = await _client.PutAsync($"http://213.171.4.235:5000/auth/giverole/{userid}", info);

            string output = await userResponse.Content.ReadAsStringAsync();

            await Login(user);

            return JsonSerializer.Deserialize<InfoModel>(output, CaseInsensitive);

        }
        catch (Exception ex)
        {
            return new InfoModel { message = ex.Message };
        }
        finally
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserAppInfo.UserData.Token);
        }
    }

    
    public async Task<InfoModel> ResetPassword(string name, string eMail)
    {
        try
        {
            string json = JsonSerializer.Serialize(new { eMail, name });
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"user/resetpassword", data);



            string result = await response.Content.ReadAsStringAsync();
            var er =  JsonSerializer.Deserialize<InfoModel>(result, CaseInsensitive);

            if (response.IsSuccessStatusCode)
            {
                return er;
            }
            throw new Exception(er.message);
        }
        catch (Exception ex)
        {
            return new InfoModel { message = ex.Message };
        }
    }



    public async Task<InfoModel> SetNewPasssword(string name, NewPasswordDto newPassword)
    {
        string json = JsonSerializer.Serialize(new { name, newPassword.verificationCode, newPassword.password1, newPassword.password2 });
        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync($"user/newpassword", data);

        string result = await response.Content.ReadAsStringAsync();
        var er = JsonSerializer.Deserialize<InfoModel>(result, CaseInsensitive);

        if (response.IsSuccessStatusCode)
        {
            return er;
        }
        throw new Exception(er.message);
    }


    public void Dispose()
    {
        _client?.Dispose();
    }

    
}