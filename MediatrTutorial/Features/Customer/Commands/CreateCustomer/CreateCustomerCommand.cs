namespace MediatrTutorial.Features.Customer.Commands.CreateCustomer;

using Dto;
using MediatR;

public record CreateCustomerCommand(string FirstName, string LastName) : IRequest<CustomerDto>;