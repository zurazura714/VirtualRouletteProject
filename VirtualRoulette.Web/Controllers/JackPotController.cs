using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VirtualRoulette.Common.Abstractions.Services;
using VirtualRoulette.Domain.Domains.Enums;
using VirtualRoulette.Web.Filters;

namespace VirtualRoulette.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[CustomAuthorize]
    public class JackPotController : Controller
    {
        private readonly IJackPotService _jackPotService;

        public JackPotController(IJackPotService jackPotService)
        {
            _jackPotService = jackPotService;
        }

        [HttpGet]
        [Route("GetJackPot")]
        public IActionResult GetJackPot()
        {
            var jackPot = _jackPotService.Set().Where(a => a.Status == JackpotStatus.Active).FirstOrDefault();
            if (jackPot != null)
                return Ok(new { JackPotAmount = jackPot.JackPotAmount });
            else
                return NotFound();
        }
    }
}