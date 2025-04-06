using KantorAPI.Pobieranie.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KantorAPI.Controllers
{
    [Route("api/download")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        IDownload _download;
        public DownloadController(IDownload download)
        {
            _download = download;
        }

        [HttpGet]
        public async Task<IActionResult> DataFromECB()
        {
            var status = await _download.DownloadData();

            if (status)
            {
                return Ok($"Status pobierania {status}, dane zostały pomyślnie pobrane ze strony Europejskiego Banku Centralnego");
            }

            return BadRequest(status);
        }
    }
}
