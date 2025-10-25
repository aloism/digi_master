using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Interfaces;
using AspnetCoreMvcFull.Interfaces.stub;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Services;
using AspnetCoreMvcFull.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Connect to the database
//builder.Services.AddDbContext<AspnetCoreMvcFullContext>(options =>
// options.UseSqlite(builder.Configuration.GetConnectionString("AspnetCoreMvcFullContext") ?? throw new InvalidOperationException("Connection string 'AspnetCoreMvcFullContext' not found.")));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AspnetCoreMvcFullContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AspnetCoreMvcFullContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
  options.Cookie.Name = ".DigiMaster.Session";
  options.IdleTimeout = TimeSpan.FromSeconds(1440);
  options.Cookie.IsEssential = true;
});
builder.Services.AddHttpClient(); // Basic registration// Add the services to the container.
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAPIService, SmsService>();
builder.Services.AddScoped<IGoogleFunctionsService, GoogleFunctionsService>();
var app = builder.Build();

// Create a service scope to get an AspnetCoreMvcFullContext instance using DI and seed the database.
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;

  SeedData.Initialize(services);
}

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

app.UseAuthorization();
app.UseSession();
app.UseMiddleware<SessionTimeoutMiddleware>();
app.Use(async (context, next) =>
{
  if (context.Session.Keys.Count() == 0 && context.Request.Path != "/")
  {
    context.Response.Redirect("/");
    return;
  }

  await next();
});

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Dashboards}/{action=Index}/{id?}"); // <-- Update in AspnetCoreMvcStarter

//app.UseEndpoints(endpoints =>
//{
// Route 1: Default route
app.MapControllerRoute(
      name: "default",
      pattern: "{controller=Dashboards}/{action=Index}/{id?}");

// Route 2: Custom route
app.MapControllerRoute(
    name: "login",
    pattern: "{controller=Auth}/{action=LoginAccount}/{id?}");

// Route 3: API route
app.MapControllerRoute(
    name: "api",
    pattern: "api/{controller=Api}/{action=Get}/{id?}");
//});

//var builder = WebApplication.CreateBuilder(args);

//// Connect to the database
//builder.Services.AddDbContext<AspnetCoreMvcFullContext>(options =>
//    options.UseSqlite(builder.Configuration.GetConnectionString("AspnetCoreMvcFullContext") ?? throw new InvalidOperationException("Connection string 'AspnetCoreMvcFullContext' not found.")));

//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Create a service scope to get an AspnetCoreMvcFullContext instance using DI and seed the database.
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    SeedData.Initialize(services);
//}

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Auth}/{action=LoginCover}/{id?}"); // <-- Update in AspnetCoreMvcStarter

app.Run();
