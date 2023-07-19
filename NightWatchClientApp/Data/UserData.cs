using NightWatchClientApp.DevsFeatures;
using NightWatchClientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.Data;

public class UserData: IUserData
{
    private List<User> userList = new List<User>();


    public UserData()
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
        if(DevMain.IsDev)
        {
            AddUser(user);
            return true;
        }
        return false;
    }


    public bool AddUser(User user)
    {
        if (DevMain.IsDev)
        {
            userList.Add(user);
            return true;
        }
        else
        {

        }

        return false;
    }


    public async Task<User> Get()
    {    
        return (await GetAll()).First();
    }

    public async Task<List<User>> GetAll()
    {
        if (DevMain.IsDev)
        {
            return userList;
        }
        else
        {
            throw new Exception();
        }
    }
    
}
