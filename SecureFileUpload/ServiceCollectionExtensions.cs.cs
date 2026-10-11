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
            services.AddOptions<FileUploadOptions>().Configure(configureOptions);
            services.AddSingleton<FileExtensionValidator>();
            return services;
        }
    }
}
