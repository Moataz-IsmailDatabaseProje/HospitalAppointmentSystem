using Microsoft.EntityFrameworkCore;

namespace HastaneRandevuSistemi.Models
{
    public class EFHastaneRandevuContext: DbContext
    {
        public DbSet<AnaBilimDali> AnaBilimDaliler { get; set; }
        public DbSet<Doktor> Doktorlar { get; set; }
        public DbSet<Kullanici> Kullaniciler { get; set; }
        public DbSet<Poliklinik> Poliklinikler { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public EFHastaneRandevuContext(DbContextOptions<EFHastaneRandevuContext> options) : base(options)
        {

        }
    }
}
