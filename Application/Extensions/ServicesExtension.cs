using Application.Services;
using Application.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {

        services.AddScoped<IAccountService, AccountService>();

        return services;
    }
}