using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NightWatchClientApp.Data.Services;

public class TeamData : ITeamData, IDisposable
{

    private readonly static string address = UserData.deviceAddress + "/teams/";

    public static readonly HttpClient _client = new HttpClient()
    {
        BaseAddress = new Uri(address)
    };
    private JsonSerializerOptions CaseInsensitive = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    private int delay = 1000;

    public TeamData()
    {

    }

    public async Task<ErrorModel> JoinTeam(TeamCreateDto team)
    {

        string json = JsonSerializer.Serialize(team);

        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserAppInfo.UserData.Token);


        HttpResponseMessage response = await _client.PostAsync($"invite/{UserAppInfo.UserData.Id}", data);

        string result = await response.Content.ReadAsStringAsync();


        if (response.IsSuccessStatusCode)
        {
            var u = JsonSerializer.Deserialize<TeamTransfer>(result, CaseInsensitive);
            UserAppInfo.TeamData = u.Team;
            return null;
        }

        var er = JsonSerializer.Deserialize<ErrorModel>(result, CaseInsensitive);
        return er;
    }




    public async Task<ErrorModel> CreateTeam(TeamCreateDto team)
    {



        string json = JsonSerializer.Serialize(team);

        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserAppInfo.UserData.Token);


        HttpResponseMessage response = await _client.PostAsync($"new/{UserAppInfo.UserData.Id}", data);

        string result = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var u = JsonSerializer.Deserialize<TeamTransfer>(result, CaseInsensitive);
            UserAppInfo.TeamData = u.Team;
            return null;
        }

        var er = JsonSerializer.Deserialize<ErrorModel>(result, CaseInsensitive);
        return er;
    }






    public void Dispose()
    {
        _client.Dispose();
    }

    public async Task LogOutTeam()
    {
        string json = JsonSerializer.Serialize(new
        {
            capId = UserAppInfo.TeamData.captain._id,
            teamId = UserAppInfo.TeamData._id
        });


        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserAppInfo.UserData.Token);


        HttpResponseMessage response = await _client.PostAsync($"kick/{UserAppInfo.UserData.Id}", data);

        string result = await response.Content.ReadAsStringAsync();

    }

    public async Task<bool> UpdateTeam(CancellationToken ct)
    {
        if (UserAppInfo.TeamData == null || string.IsNullOrEmpty(UserAppInfo.TeamData._id)) return false;



        HttpResponseMessage response = await _client.GetAsync($"team/{UserAppInfo.TeamData._id}");
        string result = await response.Content.ReadAsStringAsync();

        ct.ThrowIfCancellationRequested();
        if (response.IsSuccessStatusCode)
        {
            var u = JsonSerializer.Deserialize<Team>(result, CaseInsensitive);
            UserAppInfo.TeamData = u;
            return true;
        }

        return false;
    }

}

public interface ITeamData
{
    Task<ErrorModel> CreateTeam(TeamCreateDto data);
    Task<bool> UpdateTeam(CancellationToken ct);
    Task<ErrorModel> JoinTeam(TeamCreateDto team);
    Task LogOutTeam();
}