using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.Models.DTOs;

public class ErrorModel
{
    public string message { get; set; } = null;

    public bool IsEmpty()
    {
        return message == null;
    }
    public ErrorModel()
    {
            
    }
    public ErrorModel(string m)
    {
        message = m;
    }
}
