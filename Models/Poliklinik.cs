namespace HastaneRandevuSistemi.Models
{
    public class Poliklinik
    {
        public int Id { get; set; }
        public string Adi { get; set; }

        // Poliklinik ile Doktor arasında birçok ilişki olabilir
        public List<Doktor> Doktorlar { get; set; }
        public List<Randevu> Randevular { get; set; }
    }

}
