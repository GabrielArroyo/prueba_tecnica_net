using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly DataService _dataService;

        public DataController(DataService dataService)
        {
            _dataService = dataService;
        }
        
        [HttpPost]
        public async Task<IActionResult> SaveData()
        {
            try
            {
                await _dataService.SaveDataFromApiAsync();
                return Ok(new { Message = "Datos guardados correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet()]
        public async Task<IActionResult> GetDataById(string? bic)
        {
            var data = await _dataService.GetDataById(bic);

            return Ok(data);
        }
    }
}
