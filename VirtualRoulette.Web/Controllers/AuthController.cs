using System;
using Microsoft.AspNetCore.Mvc;
using VirtualRoulette.Common.Abstractions.Services;
using VirtualRoulette.Domain.Domains;

namespace VirtualRoulette.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISessionService _sessionService;

        public AuthController(IUserService userService, ISessionService sessionService)
        {
            _userService = userService;
            _sessionService = sessionService;
        }

        [HttpPost]
        public IActionResult Login(Login model)
        {
            //Check Username and Password
            var user = _userService.GetUser(model);

            if (user != null)
            {
                _sessionService.Save(new SessionToken() { AppUser = user.User, Token = user.Token, CreateDate = DateTime.Now, LastRequest = DateTime.Now });

                return Ok(user);
            }
            return Unauthorized();
        }
    }
}