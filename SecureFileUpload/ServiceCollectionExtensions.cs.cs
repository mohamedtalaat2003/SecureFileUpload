using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureFileUpload
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSecureFileUpload(this IServiceCollection services, Action<FileUploadOptions> configureOptions)
        {

            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configureOptions);

            services.AddOptions<FileUploadOptions>().Configure(configureOptions)
                .Validate(
                 options => options.AllowedExtensions.Count > 0,
                 "At least one allowed extension must be configured.")
                 .ValidateOnStart();

            services.AddScoped<IFileExtensionValidator, FileExtensionValidator>();

            return services;
        }
    }
}
