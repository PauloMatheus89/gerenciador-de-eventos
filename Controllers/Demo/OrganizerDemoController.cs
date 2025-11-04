using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult Create(Organizer organizer)
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

            _organizerService.AddOrganizer(organizer);

            return Content($"{organizer}");
        }

        [HttpDelete("remove-organizer")]
        public IActionResult Delete(Organizer organizer)
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

            _organizerService.RemoveOrganizer(organizer);

            return Content($"Organizer Removed Sucessfully");
        }

        [HttpPut("update-organizer")]
        public IActionResult Update(int id,Organizer organizer)
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

            _organizerService.UpdateOrganizer(id,organizer);

            return Content($"{organizer}");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}