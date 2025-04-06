namespace KantorAPI.Pobieranie.Interfaces
{
    public interface IDownload
    {
        public Task<bool> DownloadData();
    }
}
