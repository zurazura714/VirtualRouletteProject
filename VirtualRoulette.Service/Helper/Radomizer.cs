using System;

namespace VirtualRoulette.Service.Helper
{
    public class Radomizer
    {
        public static int ReturnRandomNumber()
        {
            Random random = new Random();
            return random.Next(1, 36);
        }
    }
}
