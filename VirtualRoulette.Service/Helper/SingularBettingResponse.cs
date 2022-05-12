using ge.singular.roulette;
using VirtualRoulette.Commons.POCO;
using VirtualRoulette.Domain.Domains.Enums;

namespace VirtualRoulette.Service.Helper
{
    public class SingularBettingResponse
    {
        public static BetResponse BetResponseWonORLost(int winnum, string bet)
        {

            //String bet = "[{\"T\": \"v\", \"I\": 20, \"C\": 1, \"K\": 1}]";

            IsBetValidResponse ibvr = CheckBets.IsValid(bet);
            var result = new BetResponse();
            if (ibvr.getIsValid())
            {
                int estWin = CheckBets.EstimateWin(bet, winnum);
                if (estWin > 0)
                {
                    result = new BetResponse { WinningNumber = winnum, BetAmount = ibvr.getBetAmount(), WonAmount = estWin, Status = WonOrLostStatus.Won };
                }
                else
                {
                    result = new BetResponse { WinningNumber = winnum, BetAmount = ibvr.getBetAmount(), WonAmount = 0, Status = WonOrLostStatus.Lost };
                }
            }
            return result;
        }
    }
}
