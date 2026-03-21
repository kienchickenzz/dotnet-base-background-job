namespace BaseBackgroundJob.Application.Common.Messaging;

using MediatR;

using BaseBackgroundJob.Domain.Common;


public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
