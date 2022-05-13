using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VirtualRoulette.Common.Abstractions.Services;
using VirtualRoulette.Commons.POCO;
using VirtualRoulette.Domain.Domains;
using VirtualRoulette.Domain.Domains.Enums;
using VirtualRoulette.Service.Helper;
using VirtualRoulette.Web.Filters;

namespace VirtualRoulette.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize]
    public class RoulleteController : Controller
    {
        public readonly ISpinService _spinService;
        public readonly IJackPotService _jackPotService;
        public readonly IUserService _userService;
        public readonly ISessionService _sessionService;
        public readonly IMapper _mapper;
        public RoulleteController(IUserService userService, ISessionService sessionService, ISpinService spinService, IJackPotService jackPotService, IMapper mapper)
        {
            _jackPotService = jackPotService;
            _spinService = spinService;
            _userService = userService;
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("MakeBet")]
        ///Summary
        //Input Data is json array of TICK
        ///Summary
        public IActionResult MakeBet(List<BetRequest> ticks)
        {
            var ipAddress = Request.HttpContext.Connection.LocalIpAddress.ToString();
            string token = TokenReciever.TokenReciever.Token;

            //Get Current Session And User From DB
            var currentSession = _mapper.Map<SessionTokenModel>(_sessionService.ReturnCurrentSession(token));

            //Returns Random Number
            int winnum = Radomizer.ReturnRandomNumber();
            Dictionary<int, BetResponse> betsResponses = new Dictionary<int, BetResponse>();

            ExecuTeSpinLogic(ticks, ipAddress, currentSession, winnum, betsResponses);
            return Ok(betsResponses);
        }

        [HttpPost]
        [Route("GameHistory")]
        public IActionResult GameHistory()
        {
            var token = TokenReciever.TokenReciever.Token;
            //Get current Session
            var currentSession = _mapper.Map<SessionTokenModel>(_sessionService.ReturnCurrentSession(token));

            if (currentSession != null)
            {
                var spins = _spinService.Set()
                                    .Where(a => a.AppUserID == currentSession.AppUserID);
                return Ok(spins);
            }
            return Ok("No History was found!");

            //return Ok(JsonConvert.SerializeObject(gameHistory));
        }



        private void ExecuTeSpinLogic(List<BetRequest> bets, string ipAddress, SessionTokenModel currentSession, int winnum, Dictionary<int, BetResponse> betsResponses)
        {
            int counter = 1;
            foreach (var bet in bets)
            {
                //Get User From DB
                var user = _userService.Fetch(currentSession.AppUserID);

                var betJSONResult = JsonConvert.SerializeObject(bet);
                betJSONResult = string.Format("[{0}]", betJSONResult);
                var result = SingularBettingResponse.BetResponseWonORLost(winnum, betJSONResult);

                betsResponses.Add(counter, result);

                //If Wrong Bet Request
                if (result == null)
                {
                    continue;
                }

                //Check if User has enough amount to Bet
                if (user.Balance * 100 < bet.C)
                {
                    break;
                }

                //SaveSpin
                SaveSpin(ipAddress, currentSession, result);
                UpdateUser(user, result);
                UpdateJackPot(result);
                counter++;
            }
        }

        private void UpdateJackPot(BetResponse result)
        {
            var jackPot = _jackPotService.Set().Where(a => a.Status == JackpotStatus.Active).FirstOrDefault();
            jackPot.JackPotAmount += result.BetAmount / 100;
            _jackPotService.Save(jackPot);
        }

        private void UpdateUser(AppUser user, BetResponse result)
        {
            if (result.Status == WonOrLostStatus.Won)
            {
                user.Balance += result.WonAmount - result.BetAmount;
            }
            else
            {
                user.Balance -= result.BetAmount;
            }
            _userService.Save(user);
        }

        private void SaveSpin(string ipAddress, SessionTokenModel currentSession, BetResponse result)
        {
            _spinService.Save(new Spin
            {
                IpAddress = ipAddress,
                BetAmount = result.BetAmount,
                WinningNumber = result.WinningNumber,
                WonAmount = result.WonAmount,
                AppUserID = currentSession.AppUserID,
                SessionTokenID = currentSession.ID,
                SpinDate = DateTime.Now,
                AmountForJackpot = result.BetAmount / 100,
                Status = result.Status
            });
        }
    }
}