using CoreLabBackend.Services;
using DomainLabBackend.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LabBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmpresaController : ControllerBase
    {
        private EmpresaAppService empresaAppService;

        public EmpresaController()
        {
            empresaAppService = new EmpresaAppService();
        }

        [HttpGet]
        public async Task<IActionResult> GetCountEnterprises()
        {
            int countEnterprises = await empresaAppService.GetCountEnterprisesAsync();
            return Ok(countEnterprises);
        }

        [HttpGet]
        public async Task<IActionResult> GetEnterprises()
        {
            List<EmpresaDto>? empresasDto = await empresaAppService.GetEnterprisesDtoAsync();
            return Ok(empresasDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetEnterpriseById(string id)
        {
            EmpresaDto? empresaDto = await empresaAppService.GetEnterpriseDtoAsync(id);

            if (empresaDto == null)
            {
                return NotFound();
            }

            return Ok(empresaDto);
        }

        [HttpGet]
        public async Task<IActionResult> SearchEnterprise([FromQuery] string t)
        {
            List<EmpresaDto> empresasDto = await empresaAppService.SearchEnterprisesDtoAsync(t);
            return Ok(empresasDto);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] EmpresaDto empresaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool empresaValida = empresaAppService.ValidateEmpresa(empresaDto);

            if (!empresaValida)
            {
                return UnprocessableEntity();
            }

            Empresa empresa = await empresaAppService.InsertEnterpriseAsync(empresaDto);
            return Ok(empresa.Id);
        }
    }
}