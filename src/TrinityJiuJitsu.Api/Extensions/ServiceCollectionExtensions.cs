using TrinityJiuJitsu.Application.Interfaces;
using TrinityJiuJitsu.Application.Services;
using TrinityJiuJitsu.Domain.Interfaces;
using TrinityJiuJitsu.Infrastructure.Repositories;

namespace TrinityJiuJitsu.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<IGymRepository, GymRepository>();
        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<ITrainingClassRepository, TrainingClassRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IMembershipRepository, MembershipRepository>();
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();

        // Services
        services.AddScoped<IGymService, GymService>();
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<ITrainingClassService, TrainingClassService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IMembershipService, MembershipService>();
        services.AddScoped<IAttendanceService, AttendanceService>();

        return services;
    }
}
