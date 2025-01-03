using FlatFinder.Domain.Abstractions;
using MediatR;

namespace FlatFinder.Application.Abstractions.CQRS
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {

    }
}
