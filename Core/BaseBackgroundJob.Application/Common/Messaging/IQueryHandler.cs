namespace BaseBackgroundJob.Application.Common.Messaging;

using BaseBackgroundJob.Domain.Common;

using MediatR;


public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
