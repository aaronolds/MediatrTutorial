namespace MediatrTutorial.Features.Customer.Queries.GetCustomerById;

using Dto;
using MediatR;

public record GetCustomerByIdQuery(int CustomerId) : IRequest<CustomerDto>;