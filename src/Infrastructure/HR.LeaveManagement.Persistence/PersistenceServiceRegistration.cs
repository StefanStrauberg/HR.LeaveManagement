using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services;
        }
    }
}