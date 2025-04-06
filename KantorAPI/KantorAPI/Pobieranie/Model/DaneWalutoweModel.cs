namespace KantorAPI.Pobieranie.Model
{
    public class DaneWalutoweModel
    {
        public string Currency { get; set; }
        public double Rate { get; set; }
        public DateTime Time { get; set; }
        public DateTime DownloadTime { get; set; }
    }
}
