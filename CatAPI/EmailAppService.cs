using System.Net;
using System.Net.Mail;

namespace APIs
{
    public class EmailAppService
    {
        private Requests.Email.EmailRequest request;
        private SmtpClient smtpClient;

        public EmailAppService(Requests.Email.EmailRequest request)
        {
            this.request = request;
        }

        public void SendEmail()
        {
            smtpClient = new SmtpClient(request.ServidorSmtp, 587);
            MailMessage message = new MailMessage(request.Remetente, request.Destinatario, request.Assunto, request.Mensagem);
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new NetworkCredential(request.Login, request.Senha);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Send(message);
        }
    }
}

namespace APIs.Requests.Email
{
    public class EmailRequest
    {
        public string ServidorSmtp { get; set; }

        public string Login { get; set; } = "wendelviegas@gmail.com";

        public string Senha { get; set; } = "gdzwrnbmqpslgzfo";

        public int PortaServidorSmtp { get; set; }

        public string Destinatario { get; set; }

        public string Remetente { get; set; }

        public string Assunto { get; set; }

        public string Mensagem { get; set; }
    }
}