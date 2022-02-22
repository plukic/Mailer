using Mailer.Core.Data;
using Mailer.Infrastructure.Data;
using Mailer.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MailerDbContextConfiguration
    {
        public static IServiceCollection AddConfiguredDbContext(this IServiceCollection services,
            IConfiguration configuration,bool isDevEnvironment)
        {
            var connectionString = configuration.GetConnectionString(nameof(MailerDbContext));

            services.AddDbContext<MailerDbContext>(options =>
            {
                options.UseSqlServer(connectionString);

                if (isDevEnvironment)
                {
                    options.EnableDetailedErrors(true);
                    options.EnableSensitiveDataLogging(true);
                }
            });


            services.AddScoped(typeof(IReadRepository<>), typeof(CachedRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(MyRepository<>));
            services.AddScoped(typeof(MyRepository<>));
            return services;
        }
    }
}
