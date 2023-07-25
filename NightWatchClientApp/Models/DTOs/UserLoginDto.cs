using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.Models.DTOs;


public record UserLoginDto(string email, string password);
