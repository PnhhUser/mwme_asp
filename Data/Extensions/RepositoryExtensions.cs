using Domain.Interfaces;
using Data.Repo;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Đăng ký
        services.AddScoped<IAccountRepo, AccountRepo>();


        return services;
    }
}
