using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.Models;

public class EventModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateEvent { get; set; }
    public string Status { get; set; }
    public string Image { get; set; }

    public List<Team> Teams;


    public EventModel(string Name, string Desciption, string Image)
    {
        this.Name = Name;
        this.Description = Desciption;
        this.Image = Image;
    }
}