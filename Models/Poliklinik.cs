using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace HastaneRandevuSistemi.Models
{
    public class Poliklinik
    {
        public int Id { get; set; }
        public string Adi { get; set; }

        // Poliklinik ile Doktor arasında birçok ilişki olabilir

        [JsonIgnore]
        public List<Doktor> Doktorlar { get; set; } = new List<Doktor>();
        [JsonIgnore]
        public List<Randevu> Randevular { get; set; } = new List<Randevu>();
    }

}
