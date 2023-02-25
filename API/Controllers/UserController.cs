﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private ICache<User> _cacheService;
        public UserController(IUserService userService, ICache<User> cache)
        {
            _userService = userService;
            _cacheService = cache;
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            User foundUser = _cacheService.GetOrSet("user_" + id, 60 * 4, () => _userService.FindOneUser(id));
            return foundUser != null ? new ObjectResult(foundUser) { StatusCode = StatusCodes.Status200OK } : BadRequest();
        }
        ActionResult Search(User user)
        {
            List<User> users = _userService.FindUser(user);
            return users != null ? new ObjectResult(users) { StatusCode = StatusCodes.Status200OK } : BadRequest();
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] User user)
        {
            User addedUser = _userService.AddUser(user);
            return addedUser != null ? new ObjectResult(addedUser) { StatusCode = StatusCodes.Status201Created } : BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            User deletedUser = _userService.DeleteUser(id);
            return deletedUser != null ? Ok() : BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] User user)
        {
            User updatedUser = _userService.UpdateUser(id, user);
            return updatedUser != null ? new ObjectResult(updatedUser) { StatusCode = StatusCodes.Status200OK } : BadRequest();
        }
        
        [HttpGet]
        public IActionResult getAll()
        {
            List<User> users = _userService.getAllUsers();
            return users != null ? new ObjectResult(users) { StatusCode = StatusCodes.Status200OK } : BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User user)
        {
            string token = _userService.AuthenticateUser(user);
            if(token == null)
            {
                return Unauthorized();
            }
            string json = JsonConvert.SerializeObject(new { token = token });
            return Ok(json);
        }

    }
}
