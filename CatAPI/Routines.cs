using Newtonsoft.Json.Linq;
using APIs.Requests.Email;

namespace APIs
{
    public class Routines
    {
        public async Task VerificaValorDolar(string destinatarioEmail, string remetenteEmail)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://exchange-rate-api1.p.rapidapi.com/latest?base=USD"),
                Headers =
                {
                    { "X-RapidAPI-Key", "412fc26619mshb834452d3e0598ep1794b6jsnd0976d91d998" },
                    { "X-RapidAPI-Host", "exchange-rate-api1.p.rapidapi.com" },
                },
            };

            string body = string.Empty;

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
            }

            JObject jObject = JObject.Parse(body);
            JToken? valorDolarRealJToken = jObject.SelectToken("rates.BRL");
            double.TryParse(valorDolarRealJToken.ToString(), out double valorDolarReal);

            new EmailAppService(new EmailRequest()
            {
                Assunto = "Valor Dólar",
                Destinatario = destinatarioEmail,
                Mensagem = $"Dólar está valento {valorDolarReal} Reais. Compre no site https://www.santander.com.br/cambio?utm_urlsuffix=M15605-29058-5&gclid=Cj0KCQiA3uGqBhDdARIsAFeJ5r1NKqz6SyBC5KVapgPExwrqcwKrnzGg-wdnVNg7FOEMNpQvyL0QmpIaAqSOEALw_wcB",
                PortaServidorSmtp = 587,
                ServidorSmtp = "smtp.gmail.com",
                Remetente = remetenteEmail
            }).SendEmail();
        }
    }
}

