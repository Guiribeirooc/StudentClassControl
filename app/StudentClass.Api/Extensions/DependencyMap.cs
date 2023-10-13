using StudentClass.Domain.Interfaces;
using StudentClass.Domain.Repositories;
using StudentClass.Domain.Services;
using StudentClassDomain.Repositories;
using StudentClassInfra.Configuration;

namespace StudentClassApi.Extensions
{
    public static class DependencyMap
    {
        public static void RepositoryMap(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionConfig, ConnectionConfig>(sp =>
            {
                return new(Environment.GetEnvironmentVariable("SQLSERVER_CONNECTIONSTRING"));
            });

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IRelateClassService, RelateClassService>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IRelateClassRepository, RelateClassRepository>();
        }
    }
}
