using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.Data.Managers;



public interface IEventManager
{
    public IEnumerable<EventModel> Events { get; }

    void AddEvent(EventModel eventModel);
    void AddTeam(string eventId, Team team);
    void RemoveTeam(string eventId, string teamId);
    void AddTask(string eventId, TaskModel task);
    void Refresh(List<EventModel> list);
    void RemoveEvent();

}

public class EventManager : IEventManager
{
    private List<EventModel> _events = new List<EventModel>();
    public IEnumerable<EventModel> Events => _events;

    public void AddEvent(EventModel eventModel)
    {
        _events.Add(eventModel);
    }
    public void AddTeam(string eventId, Team team)
    {

        var e = _events.Find(x => x._id == eventId);
        e.members.Add(team);
    }
    public void RemoveTeam(string eventId, string teamId)
    {
        EventModel r = _events.Find(x => x._id == eventId);
        r.members.Remove(r.members.First(x => x._id == teamId));
    }

    public void AddTask(string eventId, TaskModel task)
    {
        var e = _events.Find(x => x._id == eventId);
        e.questions.Add(task);
    }
    public void Refresh(List<EventModel> list)
    {
        _events = list;
    }

    public void RemoveEvent()
    {
        _events.Remove(_events.Last());
    }

}