using ApiGetNet.Aplicacao.Servico;
using ApiGetNet.Dominio.Modelo.Card;
using Microsoft.AspNetCore.Mvc;

namespace WebApiPayments.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GetNetController : ControllerBase
    {
        [HttpPost(Name = "ValidateCard")]
        public async Task<ActionResult> ValidateCardAsync([FromBody] ValidateNumberCardRequest request)
        {
            ValidateNumberCardResponse validateNumberCardResponse = await CardAppServico.ValidateCard(request);

            if (validateNumberCardResponse == null || validateNumberCardResponse.status != "VERIFIED")
            {
                return BadRequest(validateNumberCardResponse);
            }

            return Ok(validateNumberCardResponse);
        }

        [HttpPost(Name = "DoPayment")]
        public async Task<ActionResult> DoPayment([FromBody] CartaoCredito request)
        {
            await CardAppServico.DoPayment();

            return Ok();
        }
    }
}