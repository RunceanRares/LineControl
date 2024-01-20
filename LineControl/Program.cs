using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Web;
using System.Globalization;
using System.Security.Claims;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
  var builder = WebApplication.CreateBuilder(args);
  builder.Services.AddKendo();

  builder.Services.AddRazorPages();

  builder.Logging.ClearProviders();

  // Add services to the container.
  builder.Services.AddControllersWithViews();

  builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.Negotiate.NegotiateDefaults.AuthenticationScheme)
    .AddNegotiate();

  builder.Services.AddAuthorization(options =>
  {
    options.FallbackPolicy = options.DefaultPolicy;
  });

  builder.Logging.ClearProviders();
  builder.Host.UseNLog();

  var app = builder.Build();

  var cultureInfo = new CultureInfo("en-US");

  app.UseRequestLocalization(new RequestLocalizationOptions
  {
    DefaultRequestCulture = new RequestCulture(cultureInfo),
    SupportedCultures = new List<CultureInfo> { cultureInfo },
    SupportedUICultures = new List<CultureInfo> { cultureInfo }
  });

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

  app.Run();
}
catch (Exception exception)
{
    Console.WriteLine(exception);
}