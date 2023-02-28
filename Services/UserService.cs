using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet_Assignment.Models;
namespace Dotnet_Assignment.Services
{
    public class UserService:IUserService
    {
       private ProjectContext _context;
    public UserService(ProjectContext context) {
        _context = context;
    } 

   
    public ResponseModel DeleteUser(int userId)
    {
        ResponseModel model = new ResponseModel();
        try {
            User _temp = GetUserDetailsById(userId);
            if (_temp != null) {
                _context.Remove < User > (_temp);
                _context.SaveChanges();
                model.IsSuccess = true;
                model.Messsage = "User Removed Successfully";
            } else {
                model.IsSuccess = false;
                model.Messsage = "User Not Found";
            }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public User GetUserDetailsById(int userId)
    {
        User usr;
        try {
            usr = _context.Find < User > (userId);
        } catch (Exception) {
            throw;
        }
        return usr;
    }

   

      public List<User> GetUsersList()
    { 
         List < User > userList;
        try {
            userList = _context.Set < User > ().ToList();
        } catch (Exception) {
            throw;
        }
        return userList;
    }
  

    public ResponseModel SaveUser(User userModel)
    {
        ResponseModel model = new ResponseModel();
        try {
            //Employee _temp = GetEmployeeDetailsById(employeeModel.EmployeeId);
            // if (_temp != null) {
            //     _temp.Designation = employeeModel.Designation;
            //     _temp.EmployeeFirstName = employeeModel.EmployeeFirstName;
            //     _temp.EmployeeLastName = employeeModel.EmployeeLastName;
            //     _temp.Salary = employeeModel.Salary;
            //     _context.Update < Employee > (_temp);
            //     model.Messsage = "Employee Update Successfully";
            // } else {
                User usr = new User(){
                    Username = userModel.Username,
                    Password=userModel.Password,
                    Roles=userModel.Roles  
                };
                _context.Add < User > (usr);
                model.Messsage = "User Inserted Successfully";
           // }
            _context.SaveChanges();
            model.IsSuccess = true;
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }
    }
}