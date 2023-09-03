using CoreLabBackend.Services;
using DomainLabBackend.Domain;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static DomainLabBackend.Mapper.Mappping;

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
        public ActionResult GetCountEnterprises()
        {
            List<EmpresaDto>? empresasDto = empresaAppService.GetEnterprisesDto();

            if (empresasDto?.Count > 0)
            {
                return Ok(empresasDto.Count);
            }

            return Ok();
        }

        [HttpGet]
        public ActionResult GetEnterprises()
        {
            List<EmpresaDto>? empresasDto = empresaAppService.GetEnterprisesDto();
            return Ok(empresasDto);
        }

        [HttpGet]
        public ActionResult GetEnterpriseById(string id)
        {
            EmpresaDto? empresaDto = empresaAppService.GetEnterpriseDto(id);

            if (empresaDto == null)
            {
                return NotFound();
            }

            return Ok(empresaDto);
        }

        [HttpGet]
        public ActionResult SearchEnterprise([FromQuery] string t)
        {
            List<EmpresaDto> empresasDto = empresaAppService.SearchEnterprisesDto(t);
            return Ok(empresasDto);
        }

        [HttpPost]
        public ActionResult Insert([FromBody] EmpresaDto empresaDto)
        {
            bool empresaValida = empresaAppService.ValidateEmpresa(empresaDto);

            if (empresaValida == false)
            {
                return UnprocessableEntity();
            }

            Empresa empresa = empresaAppService.InsertEnterprise(empresaDto);
            return Ok(empresa.Id);
        }

        [HttpPut]
        public ActionResult Update([FromBody] JObject obj)
        {
            return Ok();
        }
    }
}