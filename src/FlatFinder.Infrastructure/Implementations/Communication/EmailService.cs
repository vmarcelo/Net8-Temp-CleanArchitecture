using FlatFinder.Application.Abstractions.Communication;
using FlatFinder.Domain.Users;

namespace FlatFinder.Infrastructure.Implementations.Communication
{
    internal sealed class EmailService : IEmailService
    {
        public Task SendAsync(Email recipient, string subject, string body)
        {
            //here should go the email sending implementation
            return Task.CompletedTask;
        }
    }
}
