using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NightWatchClientApp.Data.Services;



public interface IEventData
{
    Task<EventModel> CreateEvent(string name, string description);
    Task<TaskModel> AddQuestionToEvent(string eventId, TaskModel question);
    Task<EventModel> GetEvent(string eventId);
    Task<IEnumerable<EventModel>> GetEventsByHostId(string eventId);
    Task<List<EventModel>> GetAllEvents();
    Task<EventModel> AddTeamToEvent(string teamId, string eventId);
    Task<ErrorModel> DeleteEvent(string eventId);

}

public sealed class EventData : IEventData
{

    private readonly static string address = UserData.deviceAddress + "/events/";

    public static readonly HttpClient _client = new HttpClient()
    {
        BaseAddress = new Uri(address)
    };
    private JsonSerializerOptions CaseInsensitive = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };


    public EventData()
    {

    }

    public async Task<EventModel> CreateEvent(string name, string description)
    {

        string json = JsonSerializer.Serialize(new { name, description });

        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserAppInfo.UserData.Token);


        HttpResponseMessage response = await _client.PostAsync($"new/{UserAppInfo.UserData.Id}", data);

        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(JsonSerializer.Deserialize<ErrorModel>(result, CaseInsensitive).message);
        }

        var eventModel = JsonSerializer.Deserialize<EventTransfer>(result, CaseInsensitive);

        return eventModel.Event;
    }

    public async Task<TaskModel> AddQuestionToEvent(string eventId, TaskModel question)
    {
        string json = JsonSerializer.Serialize(new { question.question, question.answer, question.clue });

        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserAppInfo.UserData.Token);


        HttpResponseMessage response = await _client.PostAsync($"newtask/{eventId}", data);

        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(JsonSerializer.Deserialize<ErrorModel>(result, CaseInsensitive).message);
        }

        var eventModel = JsonSerializer.Deserialize<TaskTransfer>(result, CaseInsensitive);

        return eventModel.task;
    }

    public async Task<List<EventModel>> GetAllEvents()
    {

        HttpResponseMessage response = await _client.GetAsync($"all");

        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(JsonSerializer.Deserialize<ErrorModel>(result, CaseInsensitive).message);
        }

        var events = JsonSerializer.Deserialize<List<EventModel>>(result, CaseInsensitive);

        return events;
    }

    public async Task<EventModel> GetEvent(string eventId)
    {
        var events = await GetAllEvents();
        return events.Where(x => x._id == eventId).FirstOrDefault();
    }

    public async Task<IEnumerable<EventModel>> GetEventsByHostId(string id)
    {
        var events = await GetAllEvents();
        return events.Where(x => x.host.Id == id);
    }


    public async Task<EventModel> AddTeamToEvent(string teamId, string eventId)
    {
        string json = JsonSerializer.Serialize(new { teamId, hostId = UserAppInfo.UserData._id });

        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserAppInfo.UserData.Token);


        HttpResponseMessage response = await _client.PostAsync($"addteam/{eventId}", data); ;

        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(JsonSerializer.Deserialize<ErrorModel>(result, CaseInsensitive).message);
        }

        var eventModel = JsonSerializer.Deserialize<EventTransfer>(result, CaseInsensitive);

        return eventModel.Event;
    }

    public async Task<ErrorModel> DeleteEvent(string eventId)
    {
        string json = JsonSerializer.Serialize(new { hostId = UserAppInfo.UserData._id });

        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserAppInfo.UserData.Token);


        HttpResponseMessage response = await _client.PostAsync($"delete/{eventId}", data); ;

        string result = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<ErrorModel> (result, CaseInsensitive);
    }

    private async Task<string> SendRequest(string json, string param)
    {
        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserAppInfo.UserData.Token);


        HttpResponseMessage response = await _client.PostAsync($"addteam/{param}", data); ;

        return await response.Content.ReadAsStringAsync();
    }
}

