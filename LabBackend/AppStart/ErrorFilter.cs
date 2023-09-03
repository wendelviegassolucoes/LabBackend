//using System;
//using Iam.Aplicacao.Service;
//using Microsoft.AspNetCore.Mvc.Filters;

//namespace WebApiConsole.AppStart
//{
//    public class ErrorFilter : ExceptionFilterAttribute
//    {
//        public override void OnException(ExceptionContext context)
//        {
//            bool escreve_log_excecao = true;

//            if (context.Exception.GetType().Name == "ValidationException")
//            {
//                context.Result = ResponseAppService.MontaRespostaValidacao(context.Exception.Message);
//                escreve_log_excecao = false;
//            }
//            else if (context.Exception.GetType().Name == "UnauthorizedAccessException")
//            {
//                context.Result = ResponseAppService.MontaRespostaAcessoNegado();
//                escreve_log_excecao = false;
//            }
//            else
//            {
//                context.Result = ResponseAppService.MontaRespostaErro();
//            }

//            EscreveLogErros(escreve_log_excecao, context);
//        }

//        private static void EscreveLogErros(bool escreve_log_excecao, ExceptionContext context)
//        {
//            if (escreve_log_excecao == false)
//            {
//                return;
//            }

//            Session session;

//            try
//            {
//                session = new SessionAppService(context.HttpContext.Session).TakeSession();
//            }
//            catch (Exception)
//            {
//            }
//        }
//    }
//}