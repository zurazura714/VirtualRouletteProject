using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VirtualRoulette.Common.Abstractions.Services;
using VirtualRoulette.Web.Filters;

namespace VirtualRoulette.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[CustomAuthorize(sessionService:]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISessionService _sessionService;
        public UserController(IUserService userService, ISessionService sessionService)
        {
            _userService = userService;
            _sessionService = sessionService;
        }

        [HttpPost]
        [Route("getinfo")]
        public IActionResult GetInfo()
        {
            //Get username and balance from database
            var token = getToken();
            var user = _sessionService.ReturnUserAndBalance(token);
            //if user object is null then return http status code 500 (internal server error)
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(new { username = user.UserName, balance = user.Balance });
        }

        protected string getToken()
        {
            if (Request.Query.ContainsKey("token"))
            {
                return Request.Query.Where(x => x.Key == "token").FirstOrDefault().Value;
            }
            return "";
        }

    }
}