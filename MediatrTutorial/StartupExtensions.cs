namespace MediatrTutorial;

using System;
using System.IO;
using System.Reflection;
using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

public static class StartupExtensions {
    public static void AddSwagger(this IServiceCollection services) {
        services.AddSwaggerGen(c => {
            c.CustomSchemaIds(x => x.FullName);
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MediatR Tutorial", Version = "v1" });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
    }

    public static void UseErrorHandling(this IApplicationBuilder app) {
        app.UseMiddleware<ErrorHandlingMiddleware>();
    }
}