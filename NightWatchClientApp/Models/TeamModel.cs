using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.Models;


public class TeamTransfer
{
    public string Message { get; set; }
    public Team Team { get; set; }
}


public class Team
{
    public string _id { get; set; }
    public string teamName { get; set; }
    public string password { get; set; }
    public List<User> members { get; set; }
    public string eventName { get; set; }
    public List<TaskModel> tasks { get; set; }
    public int score { get; set; }
    public User captain { get; set; }
    public bool isInEvent { get; set; }
    public int __v { get; set; }
}



