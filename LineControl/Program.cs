using FluentAssertions.Common;
using LineControl.Common;
using LineControllerCore.Interface;
using LineControllerCore.Service;
using LineControllerInfrastructure;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Web;
using System.Globalization;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
  var builder = WebApplication.CreateBuilder(args);

  builder.Services.AddKendo();

  builder.Services.AddDbContext<LineContextDb>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    x => x.MigrationsAssembly("LineControllerInfrastructure")));

  builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.Negotiate.NegotiateDefaults.AuthenticationScheme)
    .AddNegotiate();

  builder.Services.AddScoped<IIdentityService, IdentityService>();
  builder.Services.AddScoped<IUserService, UserService>();
  builder.Services.AddScoped<ICompanyLocationService, CompanyLocationService>();

  builder.Services.AddAuthorization(options =>
  {
    options.FallbackPolicy = options.DefaultPolicy;
  });

  builder.Services.AddRazorPages();

  builder.Logging.ClearProviders();
  builder.Host.UseNLog();
  builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

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
finally
{
  LogManager.Shutdown();
}