using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualRoulette.Common.Abstractions.Services;
using VirtualRoulette.Domain.Domains;

namespace VirtualRoulette.Web.Filters
{
    //Create Custom Authorize class
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAuthorize : Attribute, IAuthorizationFilter
    {
        //Just 5 minutes for session
        TimeSpan SessionTime = new TimeSpan(0, 5, 0);

        private readonly ISessionService _sessionService;
        public CustomAuthorize(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        //Check token validation
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                bool jackpot = false;
                //Get token from http object from request
                string token = context.HttpContext.Request.Query.Where(x => x.Key == "token").FirstOrDefault().Value;

                if (context.HttpContext.Request.Query.ContainsKey("jackpot"))
                {
                    jackpot = bool.Parse(context.HttpContext.Request.Query.Where(x => x.Key == "jackpot").FirstOrDefault().Value);
                }


                if (token != null)
                {
                    SessionToken sessionToken = _sessionService.Set().Where(s => s.Token == token).FirstOrDefault();
                    if (sessionToken != null)
                    {
                        //Calculate difference from last active
                        TimeSpan diff = DateTime.Now - sessionToken.LastRequest;
                        //Check if user inactive about 5 minutes long
                        if (SessionTime > diff)
                        {
                            if (!jackpot)
                            {
                                //If he/she is active then update last request date
                                sessionToken.LastRequest = DateTime.Now;
                                _sessionService.Save(sessionToken);
                            }
                            return;
                        }
                    }
                }

                //If there is no token and also user inactive, then send UnauthorizedResult status
                context.Result = new UnauthorizedResult();
                return;
            }
            catch (Exception)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

        }
    }
}

