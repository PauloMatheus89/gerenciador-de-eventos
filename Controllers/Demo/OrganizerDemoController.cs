using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Interfaces.IService;
using GerenciadorEventos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GerenciadorEventos.Controllers
{
    [Route("[controller]")]
    public class OrganizerDemoController : Controller
    {
        private readonly ILogger<OrganizerDemoController> _logger;
        private readonly IOrganizerService _organizerService;

        public OrganizerDemoController(ILogger<OrganizerDemoController> logger, IOrganizerService organizerService)
        {
            _logger = logger;
            _organizerService = organizerService;
        }

        [HttpPost("create-organizer")]
        public IActionResult Create([FromQuery]OrganizerDto organizerDto)
        {
            Console.WriteLine($"DTO recebido pelo binder: {organizerDto.UserId}");

            

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

           var organizer = _organizerService.AddOrganizer(organizerDto);

            return Content($"{organizer}");
        }

        [HttpDelete("remove-organizer")]
        public IActionResult Delete(OrganizerDto organizerDto)
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

            var organizer = _organizerService.RemoveOrganizer(organizerDto);

            return Content($"Organizer {organizer} Removed Sucessfully");
        }

        [HttpPut("update-organizer/{id}")]
        public IActionResult Update([FromRoute]int id,OrganizerDto organizerDto)
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

           var organizer = _organizerService.UpdateOrganizer(id,organizerDto);

            return Content($"{organizer}");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}