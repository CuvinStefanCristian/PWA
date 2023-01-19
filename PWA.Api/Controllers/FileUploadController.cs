using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWA.Api.Data.Models;
using PWA.Api.Services;

namespace PWA.Api.Controllers
{
    public class FileUploadController: ControllerBase
    {
        private readonly ImaginiRepo _imaginiService;

        [HttpPost("upload")]
        [Authorize]
        public async Task<ActionResult<Imagine>> Upload(Imagine imagine)
        {
            try 
            {
                await _imaginiService.UpdateImagineAsync(imagine);
                return Ok(imagine); 
            }
            catch (Exception ex) 
            {
                return BadRequest(ex);
            }
            
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<List<Imagine>>> GetImagine()
        {
            List<Imagine> imagini = await _imaginiService.GetAll();
            return Ok(imagini);
        }

    }
}
