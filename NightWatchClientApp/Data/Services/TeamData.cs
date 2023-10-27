using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


namespace NightWatchClientApp.Data.Services;

public class TeamData : ITeamData, IDisposable
{

    private readonly static string address = UserData.deviceAddress + "/teams/";

    public static readonly HttpClient _client = new HttpClient()
    {
        BaseAddress = new Uri(address)
    };
    private JsonSerializerOptions CaseInsensitive = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };


    public TeamData()
    {

    }

    public async Task<InfoModel> JoinTeam(TeamCreateDto team)
    {
        HttpResponseMessage response = await SendPostRequest($"invite/{UserAppInfo.UserData.Id}", team);

        string result = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var u = JsonSerializer.Deserialize<TeamTransfer>(result, CaseInsensitive);
            UserAppInfo.TeamData = u.Team;
            return null;
        }

        var er = JsonSerializer.Deserialize<InfoModel>(result, CaseInsensitive);
        return er;
    }







    public async Task<InfoModel> CreateTeam(TeamCreateDto team)
    {
        HttpResponseMessage response = await SendPostRequest($"new/{UserAppInfo.UserData.Id}", team);

        string result = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var u = JsonSerializer.Deserialize<TeamTransfer>(result, CaseInsensitive);
            UserAppInfo.TeamData = u.Team;
            return null;
        }

        var er = JsonSerializer.Deserialize<InfoModel>(result, CaseInsensitive);
        return er;

    }


    public async Task<InfoModel> LeaveTeam()
    {
        if(UserAppInfo.TeamData == null) return null;
        var jsonObj = new
        {
            UserAppInfo.TeamData.teamName,
        };

        HttpResponseMessage response = await SendPostRequest($"leave/{UserAppInfo.UserData.Id}", jsonObj);

        string result = await response.Content.ReadAsStringAsync();
        var err = JsonSerializer.Deserialize<InfoModel>(result, CaseInsensitive);

        if (!response.IsSuccessStatusCode) throw new Exception(err.message);

        return err;
    }

    //public async Task<InfoModel> LogOutTeam()
    //{
    //    var jsonObj = new
    //    {
    //        capId = UserAppInfo.TeamData.captain?._id,
    //        teamId = UserAppInfo.TeamData._id
    //    };

    //    HttpResponseMessage response = await SendPostRequest($"kick/{UserAppInfo.UserData.Id}", jsonObj);

    //    string result = await response.Content.ReadAsStringAsync();
    //    var err = JsonSerializer.Deserialize<InfoModel>(result, CaseInsensitive);

    //    if (!response.IsSuccessStatusCode) throw new Exception(err.message);

    //    return err;
    //}

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





    public async Task<InfoModel> AnswerQuestion(string answer, string taskId, string EventId)
    {
        var jsonObj = new {
            answer,
            taskId,
            teamId = UserAppInfo.TeamData._id
        };


        HttpResponseMessage response = await SendPostRequest($"sendanswer/{EventId}", jsonObj);

        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidDataException(result);
        }
        var er = JsonSerializer.Deserialize<InfoModel>(result, CaseInsensitive);
        return er;
    }

    private async Task<HttpResponseMessage> SendPostRequest(string url, object jsonObj)
    {
        string json = JsonSerializer.Serialize(jsonObj);
        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserAppInfo.UserData.Token);

        return await _client.PostAsync(url, data);
    }



    public void Dispose()
    {
        _client.Dispose();
    }

}

public interface ITeamData
{
    Task<InfoModel> CreateTeam(TeamCreateDto data);
    Task<bool> UpdateTeam(CancellationToken ct);
    Task<InfoModel> JoinTeam(TeamCreateDto team);
    //Task<InfoModel> LogOutTeam();
    Task<InfoModel> AnswerQuestion(string answer, string taskId, string EventId);
    Task<InfoModel> LeaveTeam();


}