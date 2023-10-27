using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.Models.DTOs;

public class InfoModel
{
    public string message { get; set; } = null;
    public Errors errors { get; set; } = new Errors();

    public bool IsEmpty()
    {
        return message == null;
    }
    public InfoModel()
    {
            
    }
    public InfoModel(string m)
    {
        message = m;
    }
}
public class Errors
{
    public List<Error> errors { get; set; }
}

public class Error
{
    public string type { get; set; }
    public string value { get; set; }
    public string msg { get; set; }
    public string path { get; set; }
    public string location { get; set; }
}

