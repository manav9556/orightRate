using HttpMultipartParser;
using Microsoft.AspNetCore.Mvc;
using OrightApi.ExcelFile;
using OrightApi.Repository;

namespace OrightApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly ExcelConvert excelFile;
        private readonly SaveDataRepository _repo;


        public RateController (ExcelConvert excelFile, SaveDataRepository repo)
        {
            this.excelFile = excelFile;
            _repo = repo;
        }
        [HttpPost]
        [Route("insertRateCharts")]
        public async Task<IActionResult> InsertRateCharts([FromQuery] int clientId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded or file is empty.");
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;
                    var rateCharts = excelFile.ReadExcelData(stream, clientId);
                    await _repo.InsertRateChartsAsync(rateCharts);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing uploaded file: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error processing uploaded file.");
            }

            return Ok("File processed successfully.");
        }
    }

    }
