using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using TpaoProject1.Areas.Identity.Pages.Account.Manage; // Replace with the namespace of your EmailModel

public class EmailSender : IEmailSender
{
    private readonly EmailSenderOptions _emailOptions;

    public EmailSender(IOptions<EmailSenderOptions> emailOptions)
    {
        _emailOptions = emailOptions.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        try {
            using (var client = new SmtpClient())
            {
                client.Host = _emailOptions.Host;
                client.Port = _emailOptions.Port;
                client.EnableSsl = _emailOptions.EnableSsl;
                client.Credentials = new NetworkCredential(_emailOptions.UserName, _emailOptions.Password);

                var message = new MailMessage(_emailOptions.UserName, email, subject, htmlMessage)
                {
                    IsBodyHtml = true
                };

                await client.SendMailAsync(message);
            }
        }
        catch (Exception ex) { 
            
        }
    }
}

public class EmailSenderOptions
{
    public string Host { get; set; }
    public int Port { get; set; }
    public bool EnableSsl { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}
