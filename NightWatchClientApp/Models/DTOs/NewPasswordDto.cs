using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.Models.DTOs;


public class NewPasswordDto
{
    public string verificationCode { get; set; }
    public string password1 { get; set; }
    public string password2 { get; set; }
}