namespace HastaneRandevuSistemi.Models
{
    public class AnaBilimDali
    {
        public int Id { get; set; }
        public string Adi { get; set; }

        // Ana Bilim Dalı ile Poliklinik arasında birçok ilişki olabilir
        public List<Poliklinik> Poliklinikler { get; set; }
    }

}
