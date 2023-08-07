using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.Models.DTOs;

public record TeamCreateDto(string teamName, string password);
