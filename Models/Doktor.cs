namespace HastaneRandevuSistemi.Models
{
    public class Doktor
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }

        public int PoliklinikId { get; set; }
        public Poliklinik Poliklinik { get; set; }

        public List<Randevu> Randevular { get; set; }
    }
}
