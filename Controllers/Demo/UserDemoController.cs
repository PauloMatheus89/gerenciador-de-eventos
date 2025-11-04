using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Interfaces.IService;
using GerenciadorEventos.Models;
using GerenciadorEventos.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GerenciadorEventos.Controllers.Demo
{
    [Route("[controller]")]
    public class UserDemoController : Controller
    {
        private readonly IUserService _userService;

        private readonly ILogger<UserDemoController> _logger;

        public UserDemoController(ILogger<UserDemoController> logger,IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("create-user")]
        public IActionResult Create(User user)
        {
            if (!ModelState.IsValid)
            {
                List<string> validationErrors = new List<string>();

                foreach (var value in ModelState.Values)
                {
                    foreach (var erro in value.Errors)
                    {
                        validationErrors.Add(erro.ErrorMessage);
                    }
                }
                string.Join("\n", validationErrors);
                return BadRequest(validationErrors);
            }

            _userService.AddUser(user);

            return Content($"{user}");
        }

        [HttpDelete("remove-user")]
        public IActionResult Remove(User user)
        {
            if (!ModelState.IsValid)
            {
                List<string> validationErrors = new List<string>();

                foreach (var value in ModelState.Values)
                {
                    foreach (var erro in value.Errors)
                    {
                        validationErrors.Add(erro.ErrorMessage);
                    }
                }
                string.Join("\n", validationErrors);
                return BadRequest(validationErrors);
            }

            _userService.RemoveUser(user);
            return Content("The user was Sucefully Erased");
        }

        [HttpGet("get-user-id")]
        public IActionResult GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                List<string> validationErrors = new List<string>();

                foreach (var value in ModelState.Values)
                {
                    foreach (var erro in value.Errors)
                    {
                        validationErrors.Add(erro.ErrorMessage);
                    }
                }
                string.Join("\n", validationErrors);
                return BadRequest(validationErrors);
            }

            User? user = _userService.GetById(id);

            return Content($"{user}");
        }

        [HttpPut("update-user")]
        public IActionResult Remove(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                List<string> validationErrors = new List<string>();

                foreach (var value in ModelState.Values)
                {
                    foreach (var erro in value.Errors)
                    {
                        validationErrors.Add(erro.ErrorMessage);
                    }
                }
                string.Join("\n", validationErrors);
                return BadRequest(validationErrors);
            }

            _userService.UpdateUser(id, user);
            return Content($"Update User: {user}");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}