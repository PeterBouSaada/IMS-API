using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUserService
    {
        User AddUser(User user);
        User UpdateUser(User user);
        User DeleteUser(string id);
        List<User> FindUser(User user);
        User FindOneUser(string id);
        string AuthenticateUser(User user);
        List<User> getAllUsers();
    }
}