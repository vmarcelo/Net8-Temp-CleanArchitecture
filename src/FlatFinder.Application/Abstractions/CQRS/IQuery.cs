using FlatFinder.Domain.Abstractions;
using MediatR;

namespace FlatFinder.Application.Abstractions.CQRS
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {

    }
}
