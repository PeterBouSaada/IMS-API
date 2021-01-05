﻿using API.Models;
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
        User DeleteUser(User user);
        List<User> FindUser(User user);
    }
}
