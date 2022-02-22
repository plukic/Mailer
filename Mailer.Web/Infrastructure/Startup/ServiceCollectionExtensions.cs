using Ardalis.GuardClauses;
using Auth0.AspNetCore.Authentication;
using Mailer.Core.Cache;
using Mailer.Core.Configuration;
using Mailer.Core.DateTimes;
using Mailer.Core.Localization;
using Mailer.Core.Security.Claims;
using Mailer.Core.Security.Users;
using Mailer.Core.Timezones;
using Mailer.Core.Timezones.FromConfig;
using Mailer.Infrastructure.Cache;
using Mailer.Web.Configuration;
using Mailer.Web.Infrastructure.Security.Claims;
using Mailer.Web.Infrastructure.UI.Alerts;
using Mailer.Web.Services.Email;
using Mailer.Web.Services.ExternalLogin;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Mailer.Web.Infrastructure.Startup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfiguredMemoryCache(this IServiceCollection services, IConfiguration configuration)
        {
            // Add internal memory cache
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, CacheManager>();
            services.Configure<CacheSettings>(configuration.GetSection(nameof(CacheSettings)));

            services.AddSingleton(provider => provider.GetRequiredService<IOptions<CacheSettings>>().Value);

            return services;
        }

        public static IServiceCollection AddConfiguredLocalization(this IServiceCollection services)
        {
            services.AddPortableObjectLocalization(options => options.ResourcesPath = "Infrastructure/Localization/Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = LocalizationConfig.GetSupportedCultures().ToList();

                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.DefaultRequestCulture = new RequestCulture(LocalizationConfig.GetDefaultCulture());
                options.FallBackToParentCultures = true;
                options.FallBackToParentUICultures = true;

                // Set cookie name for cookie provider
                var cookieProvider = options.RequestCultureProviders.OfType<CookieRequestCultureProvider>().First();

                cookieProvider.CookieName = CookieNames.Culture;

                // Remove all culture providers so we can only use cookie localization
                // Remove this code if query string and language header providers are also used
                options.RequestCultureProviders.Clear();
                options.RequestCultureProviders.Add(cookieProvider);
            });

            return services;
        }

        public static IServiceCollection AddHttpContextCurrentPrincipalAccessor(this IServiceCollection services)
        {
            services
                .AddScoped<ICurrentUser, CurrentUser>()
                .AddSingleton<ICurrentPrincipalAccessor, HttpContextCurrentPrincipalAccessor>();

            return services;
        }

        /// <summary>
        /// Antiforgery token configuration
        /// </summary>
        public static IServiceCollection AddConfiguredAntiforgery(this IServiceCollection services)
        {
            services.AddAntiforgery(options =>
            {
                options.Cookie.Name = CookieNames.AntiforgeryToken;
                options.FormFieldName = $"{CookieNames.AntiforgeryToken}-FORM";
                options.HeaderName = $"{CookieNames.AntiforgeryToken}-TOKEN";
            });

            return services;
        }

        /// <summary>
        /// Registers <see cref="TempDataAlertManager"/> implementation of <see cref="IAlertManager"/>
        /// </summary>
        /// <param name="services">Service collection</param>
        public static IServiceCollection AddTempDataAlertManager(this IServiceCollection services)
        {
            Guard.Against.Null(services, nameof(services));

            services.AddScoped<IAlertManager, TempDataAlertManager>();

            return services;
        }


        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IEmailViewModelService, EmailViewModelService>();
            services.AddScoped<IExternalLoginService, Auth0ExternalLoginService>();
            return services;
        }


        public static IServiceCollection AddApplicationConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailerConfiguration>(configuration.GetSection(nameof(MailerConfiguration)));
            services.Configure<EmailConfiguration>(configuration.GetSection(nameof(EmailConfiguration)));
            services.AddSingleton(provider => provider.GetRequiredService<IOptions<MailerConfiguration>>().Value);
            services.AddSingleton(provider => provider.GetRequiredService<IOptions<EmailConfiguration>>().Value);

            return services;
        }


        public static IServiceCollection AddDateTimeHandlingServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TimezoneConfiguration>(configuration.GetSection(nameof(TimezoneConfiguration)));
            services.AddSingleton(provider => provider.GetRequiredService<IOptions<TimezoneConfiguration>>().Value);


            services.AddSingleton<ITimezoneService, TimezoneFromConfigurationProvider>();
            services.AddSingleton<IDateTimeConverter, DateTimeConverter>();
            return services;
        }

        public static IServiceCollection AddAuth0Login(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuth0WebAppAuthentication(options =>
            {

                options.Scope = "openid profile email";
                options.Domain = configuration["Auth0:Domain"];
                options.ClientId = configuration["Auth0:ClientId"];

                options.OpenIdConnectEvents = new Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectEvents();
                options.OpenIdConnectEvents.OnTokenValidated = async ctx =>
                {
                    var externalLoginService = ctx.HttpContext.RequestServices.GetRequiredService<IExternalLoginService>();
                    var claims = externalLoginService.GetOrCreateClaimsForExternalLogin(ctx);
                    if (claims.Any())
                    {
                        var appIdentity = new ClaimsIdentity(claims);
                        ctx.Principal.AddIdentity(appIdentity);
                    }
                };
            });
            return services;
        }
    }
}