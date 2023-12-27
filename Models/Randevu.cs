namespace HastaneRandevuSistemi.Models
{
    public class Randevu
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }

        // Randevu ile Doktor arasında bir ilişki
        public int DoktorId { get; set; }
        public Doktor Doktor { get; set; }

        // Randevu ile Kullanici arasında bir ilişki
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int PoliklinikId { get; set; }
        public Poliklinik Poliklinik { get; set; }
    }

}
