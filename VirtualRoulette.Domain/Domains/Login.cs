using System.ComponentModel.DataAnnotations;

namespace VirtualRoulette.Domain.Domains
{
    public class Login
    {
        [Required(ErrorMessage ="Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
