namespace HastaneRandevuSistemi.Models
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }

        // Kullanici ile Randevu arasında birçok ilişki olabilir
        public List<Randevu> Randevular { get; set; }
    }

}
