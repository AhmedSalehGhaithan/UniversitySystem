using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using University.Application.Decorator.Cache;
using University.Application.Decorator.Logging;
using University.Application.Interfaces;
using University.Application.Interfaces.StrategyInterfaces;
using University.Domain.Entities;
using University.Infrastructure.Data;
using University.Infrastructure.Implementation.Cache;
using University.Infrastructure.Implementation.Repositories;
using University.Infrastructure.Implementation.Repositories.Strategy;
namespace University.Infrastructure.DependencyInjection
{

    public static class ServicesContainer
    {
        public static IServiceCollection AddInfrastructureService(
            this IServiceCollection services, IConfiguration _config)
        {
            services.AddDbContext<UniversityDbContext>(options =>
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection")));

            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<MajorRepository>();
            services.AddScoped<StudentsRepository>();
            services.AddScoped<SubjectRepository>();
            services.AddScoped<TeacherRepository>();

            services.AddScoped<IMessageStrategy, MessageStrategy>();
            services.AddScoped<IErrorMessageStrategy, ErrorMessageStrategy>();


    

            services.AddScoped<IMajorInterface>(provider =>
                new LoggingMajorServiceDecorator(
                    new CachingMajor(
                        provider.GetRequiredService<MajorRepository>(),
                        provider.GetRequiredService<ICacheService>()
                    )
                )
            );

            services.AddScoped<IStudentsInterface>(provider =>
                new LoggingStudentServiceDecorator(
                    new CachingStudents(
                        provider.GetRequiredService<StudentsRepository>(),
                        provider.GetRequiredService<ICacheService>()
                    )
                )
            );

            services.AddScoped<ISubjectInterface>(provider =>
                new LoggingSubjectServiceDecorator(
                    new CachingSubject(
                        provider.GetRequiredService<SubjectRepository>(),
                        provider.GetRequiredService<ICacheService>()
                    )
                )
            );

            services.AddScoped<ITeacherInterface>(provider =>
                new LoggingTeacherServiceDecorator(
                    new CachingTeacher(
                        provider.GetRequiredService<TeacherRepository>(),
                        provider.GetRequiredService<ICacheService>()
                    )
                )
            );


            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Information()
               .WriteTo.Debug()
               .WriteTo.Console()
               .WriteTo.File(
                   path: $"{_config["MySerilog:FileName"]}-.txt",
                   restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                   outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss zzz} => {Message}{NewLine}{Exception}",
                   rollingInterval: RollingInterval.Day)
               .CreateLogger();

            return services;
        }
    }
}
