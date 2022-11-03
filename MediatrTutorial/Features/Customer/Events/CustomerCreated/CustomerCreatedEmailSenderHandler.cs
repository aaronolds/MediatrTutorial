namespace MediatrTutorial.Features.Customer.Events.CustomerCreated;

using System.Threading;
using System.Threading.Tasks;
using MediatR;

public class CustomerCreatedEmailSenderHandler : INotificationHandler<CustomerCreatedEvent> {
    public Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken) =>
        // IMessageSender.Send($"Welcome {notification.FirstName} {notification.LastName} !");
        Task.CompletedTask;
}