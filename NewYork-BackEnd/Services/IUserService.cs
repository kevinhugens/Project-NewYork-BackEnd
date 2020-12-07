using NewYork_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewYork_BackEnd.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
    }
}
