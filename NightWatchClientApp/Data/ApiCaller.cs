using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.Data;

public class ApiCaller
{

    public static string _baseAddress = "https://localhost111";
    public static readonly HttpClient _httpClient = new HttpClient()
    {
        BaseAddress = new Uri(_baseAddress)
    };

    public async Task<string> Call(string address)
    {
        return await _httpClient.GetStringAsync(address);
    }

}
