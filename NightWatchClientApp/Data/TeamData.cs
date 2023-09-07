using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NightWatchClientApp.Data;

public class TeamData: ITeamData, IDisposable
{

    private readonly static string address = UserData.deviceAddress + "/auth/";

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

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserAppInfo.UserData.Token);
        HttpResponseMessage response = await _client.PostAsync("registerteam", data);

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