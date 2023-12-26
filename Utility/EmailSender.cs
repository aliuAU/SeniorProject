using Microsoft.AspNetCore.Identity.UI.Services;

namespace FilmFolio.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //mail için webserver lazım(hosting)
            return Task.CompletedTask;
        }
    }
}
