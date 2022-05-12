using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualRoulette.Common.Abstractions.Services;
using VirtualRoulette.Data.Models.DBContext;
using VirtualRoulette.Domain.Domains;

namespace VirtualRoulette.Web.Filters
{
    //Create Custom Authorize class
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAuthorize : Attribute, IAuthorizationFilter
    {
        //Just 5 minutes for session
        TimeSpan SessionTime = new TimeSpan(0, 5, 0);

        //Check token validation
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                //Get Db context
                var dbContext = (RouletteDBContext)context.HttpContext.RequestServices.GetService(typeof(RouletteDBContext));
                var token = TokenReciever.TokenReciever.Token;

                if (token != null)
                {
                    SessionToken sessionToken = dbContext.SessionTokens.Where(s => s.Token == token).FirstOrDefault();
                    if (sessionToken != null)
                    {
                        //Calculate difference from last active
                        TimeSpan diff = DateTime.Now - sessionToken.LastRequest;
                        //Check if user inactive about 5 minutes long
                        if (SessionTime > diff)
                        {
                            //If he/she is active then update last request date
                            sessionToken.LastRequest = DateTime.Now;
                            dbContext.SessionTokens.Update(sessionToken);
                            dbContext.SaveChanges();
                            dbContext.SaveChanges();
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

