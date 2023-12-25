using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class Doktor
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string CalismaGunleri { get; set; }
        public string CalismaSaatleri { get; set; }

        // Doktor ile Poliklinik arasında bir ilişki
        public int PoliklinikId { get; set; }
        public Poliklinik Poliklinik { get; set; }

        // Doktor ile Randevu arasında birçok ilişki olabilir
        public List<Randevu> Randevular { get; set; }
    }

}
