using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.Models;

public class TaskModel
{
    public string question { get; set; }
    public string clue { get; set; }
    public string answer { get; set; }
    public string winner { get; set; }
    public string _id { get; set; }
}

public class TaskTransfer
{
    public string message { get; set; }
    public TaskModel task { get; set; }
}