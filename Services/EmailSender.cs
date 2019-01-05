using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Eureka.Extensions;
using Eureka.Models.ViewModels;

namespace Eureka.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {  
        public SmtpConfig smtpConfig;
        public EmailSender(SmtpConfig _smtpConfig){
            smtpConfig = _smtpConfig;
        }
        public ValidacionViewModel SendEmailAsync(string[] emails, string subject, string message)
        {
            SmtpClient client = new SmtpClient(smtpConfig.Server);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(smtpConfig.User, smtpConfig.Pass);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(smtpConfig.User);

            foreach(var email in emails)            
                if(email.isValido())                
                    mailMessage.To.Add(email);

            if(mailMessage.To.Count == 0)            
                return new ValidacionViewModel{Mensaje = "La colección no contiene distinatarios"}; 
            
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = subject;
            client.Send(mailMessage);
            return new ValidacionViewModel{Mensaje = "Correo enviado correctamente", Ok = true}; 
        }

        
    }
}
