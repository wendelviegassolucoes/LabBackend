using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;

namespace WebApiConsole.AppStart
{
    public class CustomAuthorizationFilter : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            await Task.CompletedTask;
            //string url = RequisicaoAppServico.PegaUrl(context);

            ////Para facilitar os testes, tudo que contém "teste" será liberado.
            //if (url.Equals("/Agente/Teste", StringComparison.InvariantCultureIgnoreCase) ||
            //    url.Equals("/Force1/Teste", StringComparison.InvariantCultureIgnoreCase) ||
            //    url.Equals("/Alaska/Teste", StringComparison.InvariantCultureIgnoreCase) ||
            //    url.Equals("/Crystal/Teste", StringComparison.InvariantCultureIgnoreCase))
            //{
            //    return;
            //}

            //await AutorizaUrlAsync(context);
        }

        private async Task AutorizaUrlAsync(AuthorizationFilterContext context)
        {
            try
            {
               await Task.CompletedTask;
            }
            catch (Exception)
            {
                //context.Result = RespostaAppServico.MontaRespostaCustomizada(403);
                return;
            }
        }
    }
}
