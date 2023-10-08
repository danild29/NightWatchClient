using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.Models.DTOs;

public class InfoModel
{
    public string message { get; set; } = null;

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
