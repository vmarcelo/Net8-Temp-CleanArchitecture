﻿using FlatFinder.Domain.Abstractions;
using MediatR;

namespace FlatFinder.Application.Abstractions.CQRS
{
    public interface ICommand : IRequest<Result>, IBaseCommand
    {

    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
    {

    }

    public interface IBaseCommand
    {

    }
}
