using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string? Sehir {  get; set; }
        public List<Randevu> Randevular { get; set; }

    }
}
