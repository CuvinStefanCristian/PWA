using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PWA.Api.Data.Models;
using PWA.Api.Services;
using PWA.Shared.DTOs;

namespace PWA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CladiriController: ControllerBase
    {
        private readonly CladiriRepo _cladiriService;
        private readonly IConfiguration _config;

        public CladiriController(CladiriRepo cladiriService, IConfiguration config)
        {
            _cladiriService = cladiriService;
            _config = config;
        }

        [HttpPost("insert")]
        [Authorize]
        public async Task<ActionResult<CladireDto>> Insert([FromBody] CladireDto cladireDto)
        {
            try
            {
                Cladire cladire = new Cladire();
                cladire.Tip_Strada = cladireDto.Tip_Strada;
                cladire.Denumire_Strada = cladireDto.Denumire_Strada;
                cladire.Numar=cladireDto.Numar;
                cladire.Bloc=cladireDto.Bloc;
                cladire.Scara=cladireDto.Scara;
                cladire.Stadiul_Blocului = cladireDto.Stadiul_Blocului;
                cladire.Anul_Construirii = cladireDto.Anul_Construirii;
                cladire.Regim_Inaltime=cladire.Regim_Inaltime;
                cladire.Sistemul_constructiv = cladireDto.Sistemul_constructiv;
                cladire.Numar_apartamente = cladireDto.Numar_apartamente;
                cladire.Longitudine = cladireDto.Longitudine;
                cladire.Latitudine = cladireDto.Latitudine;

                await _cladiriService.AddCladireAsync(cladire);

                cladireDto.Id = cladire.Id;
                return Ok(cladireDto);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<List<Cladire>>> GetCladiri()
        {
            List<Cladire> cladire = await _cladiriService.GetAll();
            return Ok(cladire);
        }
    }
}
