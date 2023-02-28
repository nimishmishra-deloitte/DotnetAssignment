using System;
using Microsoft.AspNetCore.Mvc;
using Dotnet_Assignment.Services;
using Dotnet_Assignment.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Dotnet_Assignment.Controllers
{
    [ApiController]
    [Route ("[controller]")]
    public class UserController:ControllerBase
    {
     IUserService _userService;
      public UserController(IUserService service) {
        _userService = service;
    }

[Authorize(Roles = "Admin")]   
[HttpGet("user")]
public IActionResult GetAllUsers() {
        try {
            var users = _userService.GetUsersList();
            if (users == null) return NotFound();
            return Ok(users);
        } catch (Exception) {
            return BadRequest();
        }
    }
   
    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("[action]/id")]
    public IActionResult GetUsersById(int id) {
        try {
            var project = _userService.GetUserDetailsById(id);
            if (project == null) return NotFound();
            return Ok(project);
        } catch (Exception) {
            return BadRequest();
        }
    }
    

   
    [HttpPost]
    [Route("[action]")]
    public IActionResult SaveUsers(User userModel) {
        try {
            var model = _userService.SaveUser(userModel);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    /// <summary>
    /// delete employee
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("[action]")]
    public IActionResult DeleteUser(int id) {
        try {
            var model = _userService.DeleteUser(id);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    }
}