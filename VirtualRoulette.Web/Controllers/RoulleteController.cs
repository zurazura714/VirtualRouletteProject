using System;
using System.Collections.Generic;
using System.Linq;
using ge.singular.roulette;
using Microsoft.AspNetCore.Mvc;

namespace VirtualRoulette.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Auth]
    public class RoulleteController : Controller
    {
        private ApplicationDbContext context;

        public RoulleteController(ApplicationDbContext context)
        {
            this.context = context;

        }

        [HttpPost]
        [Route("spin")]
        public IActionResult Spin(List<Bets> bets)
        {
            try
            {
                string token = getToken();
                BetResponse betResponse = new BetResponse()
                {
                    SpinId = 0,
                    Amount = 0,
                    WinningNumber = 0,
                    Status = "Incorrect"
                };

                if (!String.IsNullOrEmpty(token))
                {

                    int WinningNumber = 0;
                    Random random = new Random();

                    //Generate win number
                    WinningNumber = random.Next(0, 36);

                    //Get current session
                    SessionToken sessionToken = (from t in context.SessionTokens
                                                 join usr in context.AppUsers on t.AppUser.ID equals usr.ID
                                                 where t.Token == token
                                                 select new SessionToken()
                                                 {
                                                     AppUser = usr,
                                                     Token = t.Token,
                                                     ID = t.ID,
                                                     CreateDate = t.CreateDate

                                                 }).ToList().FirstOrDefault();

                    if (sessionToken != null && sessionToken.AppUser != null)
                    {

                        long winAmount = 0;
                        Spin spin = null;
                        //Generate bet json for singular library
                        string AllBetJson = BetToString(bets);
                        //check bet is or not valid
                        IsBetValidResponse validatedBets = CheckBets.IsValid(bets);
                        if (validatedBets.getIsValid())
                        {
                            //check win amount
                            winAmount = CheckBets.EstimateWin(AllBetJson, WinningNumber) * 100;

                            spin = new Spin()
                            {
                                BetAmount = validatedBets.getBetAmount() * 100,
                                WonAmount = winAmount,
                                SpinDate = DateTime.Now,
                                AppUser = sessionToken.AppUser,
                                SessionToken = sessionToken,
                                WinningNumber = WinningNumber,
                                IpAddress = Request.Host.Host,
                                AmountForJackpot = validatedBets.getBetAmount()

                            };

                            //Get active jackpot from database
                            JackInPot jackInPot = (from t in context.JackinPots
                                                   where t.Status == 1
                                                   select new JackInPot
                                                   {
                                                       ID = t.ID,
                                                       JackPotAmount = t.JackPotAmount,
                                                       Status = t.Status
                                                   }).ToList().FirstOrDefault();

                            if (jackInPot != null)
                            {
                                //add 1% from current bet to jackpot
                                jackInPot.JackPotAmount += spin.AmountForJackpot;
                                context.JackinPots.Update(jackInPot);
                            }

                            context.Spins.Add(spin);

                            //Recount user balance
                            sessionToken.AppUser.Balance = sessionToken.AppUser.Balance - validatedBets.getBetAmount() * 100;
                            sessionToken.AppUser.Balance = sessionToken.AppUser.Balance + winAmount;
                            context.AppUsers.Update(sessionToken.AppUser);
                            context.SaveChanges();
                            betResponse.SpinId = spin.ID;
                            betResponse.Amount = winAmount;
                            betResponse.WinningNumber = WinningNumber;
                            betResponse.Status = "Correct";

                        }
                    }
                }
                return Ok(JsonConvert.SerializeObject(betResponse));
            }
            catch (Exception ex)
            {
                Log.Logger("Spin:" + ex.Message);
                return StatusCode(500);
            }

        }

        [HttpPost]
        [Route("gamehistory")]
        public IActionResult GameHistory()
        {
            try
            {
                List<GameHistory> gameHistory = new List<GameHistory>();

                //Get current user and token object
                SessionToken sessionToken = (from t in context.SessionTokens
                                             join usr in context.AppUsers on t.AppUser.ID equals usr.ID
                                             where t.Token == getToken()
                                             select new SessionToken()
                                             {
                                                 AppUser = usr,
                                                 Token = t.Token,
                                                 ID = t.ID,
                                                 CreateDate = t.CreateDate

                                             }).ToList().FirstOrDefault();


                if (sessionToken != null && sessionToken.AppUser != null)
                {
                    //Get game history
                    gameHistory = (from t in context.Spins
                                   join usr in context.AppUsers on t.AppUser.ID equals usr.ID
                                   where t.AppUser.ID == sessionToken.AppUser.ID
                                   orderby t.SpinDate descending
                                   select new GameHistory()
                                   {
                                       SpinId = t.ID,
                                       WonAmount = t.WonAmount / 100,
                                       BetAmount = t.BetAmount / 100,
                                       Date = t.SpinDate.ToString("MM/dd/yyyy HH:mm:ss")
                                   }).ToList();
                }


                return Ok(JsonConvert.SerializeObject(gameHistory));
            }
            catch (Exception ex)
            {
                Log.Logger("GameHistory:" + ex.Message);
                return StatusCode(500);
            }

        }

        protected string BetToString(List<Bets> bets)
        {
            List<BetRequest> betRequests = new List<BetRequest>();
            int[] reds = new int[] { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
            int[] blacks = new int[] { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };
            try
            {
                foreach (Bets item in bets)
                {
                    if (item.Type == "N")
                    {
                        betRequests.Add(new BetRequest()
                        {
                            T = "n",
                            I = item.Number,
                            C = item.Amount / 100,
                            K = 1
                        });
                    }
                    if (item.Type == "FIRSTPART")
                    {
                        betRequests.Add(new BetRequest()
                        {
                            T = "half",
                            I = 1,
                            C = item.Amount / 100,
                            K = 1
                        });
                    }
                    if (item.Type == "SECONDPART")
                    {
                        betRequests.Add(new BetRequest()
                        {
                            T = "half",
                            I = 2,
                            C = item.Amount / 100,
                            K = 1
                        });
                    }
                    if (item.Type == "EVEN")
                    {
                        betRequests.Add(new BetRequest()
                        {
                            T = "even",
                            I = 1,
                            C = item.Amount / 100,
                            K = 1
                        });
                    }
                    if (item.Type == "RED")
                    {
                        betRequests.Add(new BetRequest()
                        {
                            T = "red",
                            I = 1,
                            C = item.Amount / 100,
                            K = 1
                        });
                    }
                    if (item.Type == "BLACK")
                    {
                        betRequests.Add(new BetRequest()
                        {
                            T = "black",
                            I = 1,
                            C = item.Amount / 100,
                            K = 1
                        });
                    }
                    if (item.Type == "ODD")
                    {
                        betRequests.Add(new BetRequest()
                        {
                            T = "odd",
                            I = 1,
                            C = item.Amount / 100,
                            K = 1
                        });
                    }
                    if (item.Type == "ONECOLUMN")
                    {
                        betRequests.Add(new BetRequest()
                        {
                            T = "twelve",
                            I = 1,
                            C = item.Amount / 100,
                            K = 1
                        });
                    }
                    if (item.Type == "TWOCOLUMN")
                    {
                        betRequests.Add(new BetRequest()
                        {
                            T = "twelve",
                            I = 2,
                            C = item.Amount / 100,
                            K = 1
                        });
                    }
                    if (item.Type == "THREECOLUMN")
                    {
                        betRequests.Add(new BetRequest()
                        {
                            T = "twelve",
                            I = 3,
                            C = item.Amount / 100,
                            K = 1
                        });
                    }
                    if (item.Type == "ONEROW")
                    {
                        betRequests.Add(new BetRequest()
                        {
                            T = "row",
                            I = 1,
                            C = item.Amount / 100,
                            K = 1
                        });
                    }
                    if (item.Type == "TWOROW")
                    {
                        betRequests.Add(new BetRequest()
                        {
                            T = "row",
                            I = 2,
                            C = item.Amount / 100,
                            K = 1
                        });
                    }
                    if (item.Type == "THREEROW")
                    {

                        betRequests.Add(new BetRequest()
                        {
                            T = "row",
                            I = 3,
                            C = item.Amount / 100,
                            K = 1
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Logger("BetToString:" + ex.Message);
            }


            return JsonConvert.SerializeObject(betRequests);
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