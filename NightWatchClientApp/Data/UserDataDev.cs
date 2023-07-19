using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.Data;



public class UserDataDev : IUserData
{
    private List<User> userList = new List<User>();


    public UserDataDev()
    {
        User user1 = new()
        {
            FullName = "Dania",
            Email = "d1",
            Id = 1,
            Password = "p1"
        };
        userList.Add(user1);
        User user2 = new()
        {
            FullName = "Dania",
            Email = "igor@login",
            Id = 2,
            Password = "p2"
        };
        userList.Add(user2);
    }

    public User Login(string email, string password)
    {
        foreach (var user in userList)
        {
            if (user.Email == email && user.Password == password)
            {

                return user;
            }
        }
        return null;
    }

    public bool Register(User user)
    {
        userList.Add(user);
        return true;
        
    }



    public async Task<User> Get()
    {
        return (await GetAll()).First();
    }

    public async Task<List<User>> GetAll()
    {
        return userList;
    }

}
