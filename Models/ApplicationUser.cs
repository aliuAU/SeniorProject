using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FilmFolio.Models
{//user database e ek özellikler
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string BirthDate { get; set; }
        public string? Address { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}
