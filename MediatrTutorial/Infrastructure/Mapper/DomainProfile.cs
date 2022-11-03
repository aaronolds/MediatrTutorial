namespace MediatrTutorial.Infrastructure.Mapper;

using System;
using AutoMapper;
using Domain;
using Dto;
using Features.Customer.Commands.CreateCustomer;

public class DomainProfile : Profile {
    public DomainProfile() {
        CreateMap<CreateCustomerCommand, Customer>()
            .ForMember(c => c.RegistrationDate, opt =>
                opt.MapFrom(_ => DateTime.Now));

        CreateMap<Customer, CustomerDto>()
            .ForMember(cd => cd.RegistrationDate, opt =>
                opt.MapFrom(c => c.RegistrationDate.ToShortDateString()));
    }
}