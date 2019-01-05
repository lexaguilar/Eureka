using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eureka.Models.ViewModels;

namespace Eureka.Services
{
    public interface IEmailSender
    {
        ValidacionViewModel SendEmailAsync(string[] email, string subject, string message);
    }
}
