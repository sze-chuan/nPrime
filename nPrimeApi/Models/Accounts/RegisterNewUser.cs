using System.ComponentModel.DataAnnotations;

namespace nPrimeApi.Models
{
    public class RegisterNewUser
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
