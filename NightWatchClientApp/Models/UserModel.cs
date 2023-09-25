
using Newtonsoft.Json;

namespace NightWatchClientApp.Models;


public class User
{
    public string Id { set { _id = value; } get { return _id; } }
    public string _id { get; set; }
    public string Name { get; set; }
    public string EMail { get; set; }
    public string Password { get; set; }
    public string avatarUrl { get; set; }
    public string Token { get; set; }
    public List<string> roles { get; set; }

    public int? TeamId { get; set; } = null;

    public string TeamName { get; set; }
    public string VerificationCode { get; set; }

    public bool isTeamMember { get; set; }
    public User()
    {
        
    }

    public User(string email, string password, string name)
    {
        EMail = email;
        Password = password;
        Name = name;
    }
}



