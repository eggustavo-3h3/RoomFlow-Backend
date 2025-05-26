using System.Net;
using System.Net.Mail;
using RoomFlowApi.Domain.DTO.Email;

namespace RoomFlowApi.Infra.Email;

public class EmailService
{
    private const string De = "no-reply@tccnapratica.com.br";
    private const string NomeExibicao = "Startly";
    private const string SmtpHost = "smtp.kinghost.net";
    private const string UserName = "no-reply@tccnapratica.com.br";
    private const string Senha = "Etec@2025";
    private const int SmtpPorta = 587;
    private const bool EnableSsl = false;

    public EnviarEmailResponse EnviarEmail(string destinatario, string assunto, string corpo, bool corpoHtml)
    {
        try
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(De, NomeExibicao)
            };

            if (destinatario.Contains(';'))
            {
                var emails = destinatario.Split(';').ToList();
                emails.ForEach(email =>
                {
                    mail.To.Add(new MailAddress(email));
                });
            }
            else
                mail.To.Add(new MailAddress(destinatario));

            mail.Subject = NomeExibicao + " Soft - " + assunto;
            mail.Body = corpo;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            //outras opções
            //mail.Attachments.Add(new Attachment(arquivo));

            using var smtp = new SmtpClient(SmtpHost, SmtpPorta);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(UserName, Senha);
            smtp.EnableSsl = EnableSsl;
            smtp.Send(mail);

            return new EnviarEmailResponse
            {
                Sucesso = true,
                Mensagem = "Email enviado com sucesso!!!"
            };
        }
        catch (Exception ex)
        {
            return new EnviarEmailResponse
            {
                Sucesso = false,
                Mensagem = ex.InnerException == null
                    ? "Erro ao enviar o e-mail: " + ex.Message
                    : "Erro ao enviar o e-mail: " + ex.Message + " - " + ex.InnerException.Message
            };
        }
    }
}