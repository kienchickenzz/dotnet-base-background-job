namespace BaseBackgroundJob.Application.Common.Messaging;

using MediatR;

using BaseBackgroundJob.Domain.Common;


public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
