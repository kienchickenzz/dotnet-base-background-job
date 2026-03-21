namespace BaseBackgroundJob.Application.Common.Messaging;

using BaseBackgroundJob.Domain.Common;


public interface IEventPublisher
{
    Task PublishAsync(IDomainEvent @event);
}
