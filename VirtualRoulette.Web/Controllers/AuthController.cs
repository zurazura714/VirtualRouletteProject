using System;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, ISessionService sessionService, IMapper mapper)
        {
            _userService = userService;
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(Login model)
        {
            //Check Username and Password
            var user = _userService.GetUser(model);

            if (user != null)
            {
                _sessionService.Save(new SessionToken() { AppUser = user.User, Token = user.Token, CreateDate = DateTime.Now, LastRequest = DateTime.Now });
                TokenReciever.TokenReciever.Token = user.Token;

                return Ok();
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("LogOut")]
        public IActionResult LogOuts()
        {
            TokenReciever.TokenReciever.Token = null;
            return Ok();
        }
    }
}