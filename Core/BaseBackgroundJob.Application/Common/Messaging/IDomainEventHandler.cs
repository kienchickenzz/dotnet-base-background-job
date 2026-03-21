namespace BaseBackgroundJob.Application.Common.Messaging;

using MediatR;

using BaseBackgroundJob.Domain.Common;


public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
