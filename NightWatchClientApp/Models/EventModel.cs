using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.Models;

public class EventModel
{
    public string _id { get; set; }
    public string name { get; set; }

    public string description { get; set; }
    public DateTime DateEvent { get; set; }
    public string Status { get; set; }
    public string Image { get; set; }

    public List<Team> members { get; set; }
    public ObservableCollection<TaskModel> questions { get; set; }
    public User host { get; set; }

    public EventModel()
    {
        
    }

    public EventModel(string Name, string Desciption, string Image)
    {
        this.name = Name;
        this.description = Desciption;
        this.Image = Image;
    }
}

public class EventTransfer
{
    public string message { get; set; }
    public EventModel Event { get; set; }
}


