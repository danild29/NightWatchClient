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
    public IEventManager EventManager { get; set; }

    Task<EventModel> CreateEvent(CreateEventDto createEventDto);
    Task<TaskModel> AddQuestionToEvent(string eventId, TaskModel question);
    Task<EventModel> GetEvent(string eventId);
    Task<IEnumerable<EventModel>> GetEventsByHostId(string eventId);
    Task<List<EventModel>> GetAllEvents();
    Task<EventModel> AddTeamToEvent(string teamName, string eventId);
    Task<InfoModel> KickTeamFromEvent(string teamId, string eventId);
    Task<InfoModel> DeleteEvent(string eventId);

    Task<EventModel> GetEventByName(string name);
}

public sealed class EventData : IEventData
{

    public IEventManager EventManager { get; set; } = new EventManager();



    private readonly static string address = UserData.deviceAddress + "/events/";

    public static readonly HttpClient _client = new HttpClient()
    {
        BaseAddress = new Uri(address)
    };
    private JsonSerializerOptions CaseInsensitive = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };


    public EventData() { }

    public async Task<List<EventModel>> GetAllEvents()
    {

        HttpResponseMessage response = await _client.GetAsync($"all");

        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(JsonSerializer.Deserialize<InfoModel>(result, CaseInsensitive).message);
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
        var hostEvents = events.Where(x => x.host.Id == id);

        EventManager.Refresh(hostEvents.ToList());
        
        return hostEvents;
    }

    
    
    public async Task<EventModel> CreateEvent(CreateEventDto createEventDto)
    {

        string json = JsonSerializer.Serialize(new { createEventDto.name, createEventDto.description, createEventDto.start, createEventDto.end });

        var res = (await SendPostRequest<EventTransfer>("new/" + UserAppInfo.UserData.Id, json)).Event;
        EventManager.AddEvent(res);
        return res;
    }


    public async Task<EventModel> AddTeamToEvent(string teamName, string eventId)
    {
        string json = JsonSerializer.Serialize(new { teamName, hostId = UserAppInfo.UserData._id });

        var res = (await SendPostRequest<EventTransfer>("addteam/"+eventId, json)).Event;
        EventManager.AddTeam(res._id, res.members.First(x => x.teamName == teamName));
        return res;
    }
    public async Task<InfoModel> KickTeamFromEvent(string teamName, string eventId)
    {
        string json = JsonSerializer.Serialize(new { teamName, hostId = UserAppInfo.UserData._id });
        var res = (await SendPostRequest<InfoModel>("kickteam/" + eventId, json));
        EventManager.RemoveTeam(eventId, teamName);
        return res;
    }

    public async Task<InfoModel> DeleteEvent(string eventId)
    {
        string json = JsonSerializer.Serialize(new { hostId = UserAppInfo.UserData._id });

        var res = await SendPostRequest<InfoModel>("delete/" + eventId, json);
        return res;
    }

    public async Task<TaskModel> AddQuestionToEvent(string eventId, TaskModel question)
    {
        string json = JsonSerializer.Serialize(new { question.question, question.answer, question.clue });
        TaskModel res = (await SendPostRequest<TaskTransfer>("newtask/" + eventId, json)).task;

        EventManager.AddTask(eventId, res);
        return res;
    }




    private async Task<T> SendPostRequest<T>(string url, string json)
    {
        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserAppInfo.UserData.Token);


        HttpResponseMessage response = await _client.PostAsync(url, data); ;

        string result = await response.Content.ReadAsStringAsync();
        if(!response.IsSuccessStatusCode)
        {
            throw new Exception(JsonSerializer.Deserialize<InfoModel>(result, CaseInsensitive).message);
        }

        return JsonSerializer.Deserialize<T>(result, CaseInsensitive);
    }

    public async Task<EventModel> GetEventByName(string name)
    {
        var res = await GetAllEvents();
        return res.Where(x => x.name == name).Single();
    }
}

