using NightWatchClientApp.DevsFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace NightWatchClientApp.Data;



public class UserDataDev //IUserData
{
    private List<User> userList = new List<User>();

    private const string userFile = @"C:\Users\Dania\source\repos\NightWatchClient\NightWatchClientApp\Data\FIlesTXT\userFile.txt";



    //public UserDataDev()
    //{
    //    DevMain.WriteToLogs(userFile);
     
    //    /
    //    try
    //    {
    //        List<string> file = File.ReadAllLines(userFile).ToList();

    //        foreach (string line in file)
    //        {
    //            var user = JsonSerializer.Deserialize<User>(line);
    //            userList.Add(user);
    //        }

    //    }
    //    catch (Exception)
    //    {

    //        //User user1 = new()
    //        //{
    //        //    Name = "Dania",
    //        //    Email = "d1",
    //        //    Id = "1",
    //        //    Password = "p1"
    //        //};
    //        //userList.Add(user1);
    //        //User user2 = new()
    //        //{
    //        //    Name = "Dania",
    //        //    Email = "igor@login",
    //        //    Id = "2",
    //        //    Password = "p2"
    //        //};
    //        //userList.Add(user2);


    //        List<string> file = new();
    //        foreach (User user in userList)
    //        {
    //            string userStr = JsonSerializer.Serialize(user);
    //            file.Add(userStr);
    //        }

    //        File.WriteAllLines(userFile, file);
    //    }
       
    //}

    //public async Task<ErrorModel> Login(UserLoginDto userToFind)
    //{
    //    foreach (var user in userList)
    //    {
    //        if (user.Email == userToFind.name && user.Password == userToFind.password)
    //        {
    //            UserAppInfo.UserData = user;
    //            return null;
    //        }
    //    }
    //    return new ErrorModel("not found");


    //}

    //public async Task<ErrorModel> Register(UserRegisterDto newUser)
    //{
    //    var user = new User(newUser.email, newUser.password, newUser.name);
    //    user.Id = (userList.Count + 1).ToString();
    //    userList.Add(user);
    //    List<string> file = new();
    //    foreach (User u in userList)
    //    {
    //        string userStr = JsonSerializer.Serialize(u);
    //        file.Add(userStr);
    //    }

    //    File.WriteAllLines(userFile, file);

    //    return null;
        
    //}



    //public async Task<User> Get()
    //{
    //    return (await GetAll()).First();
    //}

    //public async Task<List<User>> GetAll()
    //{
    //    return userList;
    //}

}
