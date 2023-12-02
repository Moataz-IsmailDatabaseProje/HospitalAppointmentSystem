using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HastaneRandevuSistemi.Models
{
    public class EFHastaneRandevuContext: IdentityDbContext<IdentityUser>
    {
        public DbSet<AnaBilimDali> AnaBilimDaliler { get; set; }
        public DbSet<Doktor> Doktorlar { get; set; }
        public DbSet<Kullanici> Kullaniciler { get; set; }
        public DbSet<Poliklinik> Poliklinikler { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public EFHastaneRandevuContext(DbContextOptions<EFHastaneRandevuContext> options) : base(options)
        {

        }
    }
}
