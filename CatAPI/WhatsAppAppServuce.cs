using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIs
{
    public class WhatsAppAppServuce
    {
        public async Task SendMessageAsync()
        {
            object language = new
            {
                code = "pt_BR"
            };

            object template = new
            {
                name = "Preço Dolar está 4.90",
                language = language
            };
            
            object post = new
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = "+5524992225456",
                type = template
            };

            var text = @"
{
    ""messaging_product"": ""whatsapp"",    
    ""recipient_type"": ""individual"",
    ""to"": ""{{Recipient-Phone-Number}}"",
    ""type"": ""text"",
    ""text"": {
        ""preview_url"": false,
        ""body"": ""text-message-content""
    }
}
";

            StringContent stringContent = new StringContent(post.ToString());

            //string body = string.Empty;

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://graph.facebook.com/v17.0/173662619161332/messages"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer EAA0NvrOmnAUBO32xTIJr3GaYfQzSi58x48qqNVbELHplDTdrc7C6MzscSKuRZBafDSm3vSkGYqRyNZA0AfwJkZBhiZCZBCTl2Nm6S2MdUkmeynKOaagLGpXdKgMlLftaJCl801CsMP9A6ZCj8DNxfq9sEW3sUnSHOKkzMXrn2JBxgwPULFNmd3ZB3QADKp9zf8HsyUbIpPmVyaURyAZD");

                    request.Content = new StringContent("{ \"messaging_product\": \"whatsapp\", \"recipient_type\": \"individual\", \"to\": \"5524992225456\", \"type\": \"text\", \"text\": { \"preview_url\": \"false\", \"body\": \"Teste\" } }");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                }
            }
        }
    }
}
        //https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages
