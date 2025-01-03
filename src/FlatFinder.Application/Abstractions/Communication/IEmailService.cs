using FlatFinder.Domain.Users;

namespace FlatFinder.Application.Abstractions.Communication
{
    public interface IEmailService
    {
        Task SendAsync(Email recipient, string subject, string body);
    }
}
