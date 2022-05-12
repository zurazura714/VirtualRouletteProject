using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VirtualRoulette.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Auth]
    public class JackPotController : Controller
    {
        private ApplicationDbContext context;

        public JackPotController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                JackInPot jackInPot = (from t in context.JackinPots
                                       where t.Status == 1
                                       select new JackInPot
                                       {
                                           ID = t.ID,
                                           JackPotAmount = t.JackPotAmount,
                                           Status = t.Status
                                       }).ToList().FirstOrDefault();

                return Ok(new { jackpotAmount = (jackInPot == null) ? 0 : jackInPot.JackPotAmount });
            }
            catch (Exception ex)
            {
                Log.Logger("JackPot Index:" + ex.Message);
                return StatusCode(500);
            }


        }
    }
}