using Auth0.AspNetCore.Authentication;
using FluentValidation.AspNetCore;
using Mailer.Web.Configuration;
using Mailer.Web.Hubs;
using Mailer.Web.Infrastructure.Startup;
using Mailer.Web.Models.Message.Validators;
using MediatR;

var builder = WebApplication
    .CreateBuilder(args);

#region Configuration Files
builder.Configuration.AddJsonFile("appsettings.configuration.json");
builder.Configuration.AddJsonFile("appsettings.dbconfig.json");
#endregion

#region MVC General Configuration
// Add services to the container.
builder.Services
    .AddControllersWithViews()
    .AddFluentValidation(options => options.RegisterValidatorsFromAssembly(typeof(MessageComposeViewModelValidator).Assembly))
    .AddViewLocalization()
    .AddDataAnnotationsLocalization()
    .AddCookieTempDataProvider(options => options.Cookie.Name = CookieNames.TempData)
    ;
#endregion

#region MVC Configuration & General Dependency injection
builder.Services.AddHttpContextAccessor()
                    .AddConfiguredMemoryCache(builder.Configuration)
                    .AddConfiguredLocalization()
                    .AddHttpContextCurrentPrincipalAccessor()
                    .AddConfiguredAntiforgery()
                    .AddTempDataAlertManager();
#endregion

#region Database
builder.Services.AddConfiguredDbContext(builder.Configuration, builder.Environment.IsDevelopment());
#endregion

#region MediatR
builder.Services.AddMediatR(typeof(Program), typeof(Mailer.Infrastructure.AssemblyTarget), typeof(Mailer.Core.AssemblyTarget));
#endregion

#region Automapper
builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(Mailer.Core.AssemblyTarget).Assembly);
#endregion
#region SignalR
builder.Services.AddSignalR();
#endregion

#region Application services 
builder.Services.AddApplicationServices()
                .AddApplicationConfigurations(builder.Configuration);

builder.Services.AddDateTimeHandlingServices(builder.Configuration);
#endregion


#region Auth0
builder.Services.AddAuth0Login(builder.Configuration);
#endregion

#region Application Runtime

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<EmailHub>("/emailHub");


app.Run();
#endregion

