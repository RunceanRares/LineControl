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
using LineControllerCore.Mapping;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using LineControllerInfrastructure.Entities;

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
  builder.Services.AddScoped<IActivityTypeService, ActivityTypeService>();
  builder.Services.AddScoped<IActiveDirectoryService, ActiveDirectoryService>();
  builder.Services.AddScoped<IUserRoleService, UserRoleService>();
  builder.Services.AddScoped<IUserGroupService, UserGroupService>();
  builder.Services.AddScoped<IDeviceService, DeviceService>();
  builder.Services.AddScoped<ILinkService, LinkService>();
  builder.Services.AddScoped<ICalibrationDevice, CalibrationDeviceService>();
  builder.Services.AddScoped<IDeviceClassMode, DeviceClassModeService>();
  builder.Services.AddScoped<IDeviceCalibrationService, DeviceCalibrationService>();

  builder.Services.Configure<IdentityOptions>(options =>
  {
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
    options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
  });

  builder.Services.AddHttpContextAccessor();

  builder.Services.AddAuthorization(options =>
  {
    options.FallbackPolicy = options.DefaultPolicy;
  });

  builder.Services.AddRazorPages();
  builder.Services.AddLogging();

  builder.Logging.ClearProviders();
  builder.Host.UseNLog();
 
  builder.Services.AddAutoMapper(typeof(ActivityTypeMapperProfile).Assembly);
  builder.Services.AddAutoMapper(typeof(UserMapperProfile).Assembly);
  builder.Services.AddAutoMapper(typeof(DeviceMapperProfiler).Assembly);
  builder.Services.AddAutoMapper(typeof(DeviceClassModeMapperProfile).Assembly);
  builder.Services.AddAutoMapper(typeof(DeviceClassMapperProfile).Assembly);
  builder.Services.AddAutoMapper(typeof(DeviceModelMapperProfile).Assembly);
  builder.Services.AddAutoMapper(typeof(DeviceCalibrationOrderMapperProfile).Assembly);
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