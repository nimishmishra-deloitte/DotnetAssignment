using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet_Assignment.Models;

namespace Dotnet_Assignment.Services
{
    public interface IUserService
    {
         List<User> GetUsersList();
     

        
        User GetUserDetailsById(int userId);

       
        ResponseModel SaveUser(User userModel);


       
        ResponseModel DeleteUser(int userId);
    }
}