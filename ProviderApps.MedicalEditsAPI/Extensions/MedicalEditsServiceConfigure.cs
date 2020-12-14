using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProviderApps.Core.Classes;
using ProviderApps.Core.Interfaces;

namespace ProviderApps.MedicalEditsAPI.Extensions
{
    public static class MedicalEditsServiceConfigure
    {
        public static IServiceCollection AddMedicalEditsService(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var serviceProvider = services.BuildServiceProvider();
            var medicalEditsSettings = serviceProvider.GetRequiredService<IOptions<TypedSettings>>()?.Value?.MedicalEditsSettings;
            if (medicalEditsSettings == null)
            {
                throw new ArgumentNullException(nameof(medicalEditsSettings));
            }
            services.AddHttpClient<IMedicalEditsClient, MedicalEditsClient>(client =>
            {
                client.BaseAddress = new Uri(medicalEditsSettings.Url);
            });

            services.AddScoped<IMedicalEditsService, MedicalEditsService>();

            return services;

        }
    }
}
