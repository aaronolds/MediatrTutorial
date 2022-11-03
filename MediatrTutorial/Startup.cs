namespace MediatrTutorial;

using AutoMapper;
using Data;
using Data.EventStore;
using FluentValidation.AspNetCore;
using Infrastructure.Behaviours;
using Infrastructure.Mapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Startup {
    public void ConfigureServices(IServiceCollection services) {
        services.AddControllers();

        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        
        services.AddMediatR(typeof(Startup).Assembly);
        services.AddSwagger();
        services.AddAutoMapper(typeof(DomainProfile).Assembly);
        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseInMemoryDatabase("MediatorDB"));

        services.AddSingleton<IEventStoreDbContext, EventStoreDbContext>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(EventLoggerBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env) {
        if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
        }

        app.UseErrorHandling();
        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI(c => {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "MediatR Tutorial");
            c.RoutePrefix = string.Empty;
        });

        app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });
    }
}