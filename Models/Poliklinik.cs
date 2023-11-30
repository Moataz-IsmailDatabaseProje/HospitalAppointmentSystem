namespace HastaneRandevuSistemi.Models
{
    public class Poliklinik
    {
        public int Id { get; set; }
        public string Adi { get; set; }

        // Poliklinik ile Doktor arasında birçok ilişki olabilir
        public List<Doktor> Doktorlar { get; set; }

        // Ana Bilim Dalı ile Poliklinik arasında bir ilişki
        public int AnaBilimDaliId { get; set; }
        public AnaBilimDali AnaBilimDali { get; set; }
    }

}
