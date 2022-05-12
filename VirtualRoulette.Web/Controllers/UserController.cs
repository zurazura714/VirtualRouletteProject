using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VirtualRoulette.Common.Abstractions.Services;
using VirtualRoulette.Commons.POCO;
using VirtualRoulette.Web.Filters;

namespace VirtualRoulette.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, ISessionService sessionService, IMapper mapper)
        {
            _userService = userService;
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetUserNameAndBalance")]
        public IActionResult GetUserNameAndBalance()
        {
            var token = TokenReciever.TokenReciever.Token;
            //Get username and balance from database
            var currentSession = _mapper.Map<SessionTokenModel>(_sessionService.ReturnCurrentSession(token));

            if (currentSession != null)
            {
                var user = _mapper.Map<UserNameAndBalanceModel>(_userService.Fetch(currentSession.AppUserID));
                if (user != null)
                    return Ok(user);
            }
            return Unauthorized();
        }
    }
}