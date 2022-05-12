using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using VirtualRoulette.Data.Models.DBContext;
using VirtualRoulette.Domain.Domains;
using VirtualRoulette.Domain.Domains.Enums;
using VirtualRoulette.Service.Helper;

namespace VirtualRoulette.Web.DataInit
{
    public static class DataInit
    {
        public static void AddData(IServiceProvider serviceScope)
        {
            var context = serviceScope.GetRequiredService<RouletteDBContext>();
            context.Database.EnsureCreated();
            if (!context.AppUsers.Any())
            {
                AppUser user = new AppUser()
                {
                    UserName = "Singular",
                    Password = Cryptography.HmacSHA256("Singular", "Singular123"),
                    Balance = 5000
                };
                context.AppUsers.Add(user);
                JackPot jackInPot = new JackPot();
                jackInPot.JackPotAmount = 0;
                jackInPot.Status = JackpotStatus.Active;
                context.JackPots.Add(jackInPot);
                context.SaveChangesAsync();
            }
        }
    }
}
