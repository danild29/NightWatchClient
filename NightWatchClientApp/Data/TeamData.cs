using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NightWatchClientApp.Data;

public class TeamData: ITeamData, IDisposable
{

    public readonly static string deviceAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
    private readonly static string address = deviceAddress + "/auth/";

    public static readonly HttpClient _client = new HttpClient()
    {
        BaseAddress = new Uri(address)
    };
    private JsonSerializerOptions CaseInsensitive = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };


    private int delay = 1000;

    public TeamData()
    {
    }

    public async Task<ErrorModel> CreateTeam(TeamCreateDto team)
    {
        await Task.Delay(delay);


        string json = JsonSerializer.Serialize(team);

        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("login", data);

        string result = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var u = JsonSerializer.Deserialize<Team>(result, CaseInsensitive);
            UserAppInfo.TeamData = u;
            return null;
        }

        var er = JsonSerializer.Deserialize<ErrorModel>(result, CaseInsensitive);
        return er;
    }






    public void Dispose()
    {
        _client.Dispose();
    }
}

public interface ITeamData
{
    Task<ErrorModel> CreateTeam(TeamCreateDto data);
}