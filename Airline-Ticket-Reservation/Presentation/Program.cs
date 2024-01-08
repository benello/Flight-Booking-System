using Application.Contracts;
using Application.Enums;
using Application.Services;
using DataAccess.Contracts;
using DataAccess.DataContext;
using DataAccess.Repositories.Db;
using DataAccess.Repositories.Json;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// An environment variable is set in the Azure App Service that contains the connection string
string connection = builder.Configuration.GetConnectionString(builder.Environment.IsDevelopment() ? "DefaultConnection" : "SQLAZURECONNSTR_DefaultConnection");

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AirlineDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<ITransaction, TransactionProvider>();

var useJson = Environment.GetEnvironmentVariable("UseJson");

if (useJson == null)
{
    builder.Services.AddDbContext<AirlineDbContext>(options => options.UseSqlServer(connection));
    
    builder.Services.AddScoped<ITicketsRepository, TicketDbRepository>();
    builder.Services.AddScoped<IFlightsRepository, FlightDbRepository>();
    builder.Services.AddScoped<ISeatsRepository, SeatDbRepository>();
    builder.Services.AddScoped<IPassportRepository, PassportDbRepository>();
}
else
{
    builder.Services.AddDbContext<AirlineDbContext>(options => options.UseInMemoryDatabase("airlinedb"));
    
    builder.Services.AddScoped<ITicketsRepository, TicketJsonRepository>(service => 
        new TicketJsonRepository(Path.Combine(service.GetService<IWebHostEnvironment>()!.WebRootPath, Path.Combine(service.GetService<FileService>()!.GetAbsoluteDirectory(FileCategory.Repository), "ticket.json")),
            service.GetService<AirlineDbContext>()!));
    builder.Services.AddScoped<IFlightsRepository, FlightJsonRepository>(service => 
        new FlightJsonRepository(Path.Combine(service.GetService<IWebHostEnvironment>()!.WebRootPath, Path.Combine(service.GetService<FileService>()!.GetAbsoluteDirectory(FileCategory.Repository), "flight.json")),
            service.GetService<AirlineDbContext>()!));
    builder.Services.AddScoped<ISeatsRepository, SeatJsonRepository>(service => 
        new SeatJsonRepository(Path.Combine(service.GetService<IWebHostEnvironment>()!.WebRootPath, Path.Combine(service.GetService<FileService>()!.GetAbsoluteDirectory(FileCategory.Repository), "seat.json")), 
            service.GetService<AirlineDbContext>()!));
    builder.Services.AddScoped<IPassportRepository, PassportJsonRepository>(service => 
        new PassportJsonRepository(Path.Combine(service.GetService<IWebHostEnvironment>()!.WebRootPath, Path.Combine(service.GetService<FileService>()!.GetAbsoluteDirectory(FileCategory.Repository), "passport.json")),
            service.GetService<AirlineDbContext>()!));
}

builder.Services.AddScoped<IAirlineService, AirlineService>();
builder.Services.AddScoped<IAdminService, AdminService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

// Seeded here for simplicity
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    await SeedRoles(roleManager);
}

app.MapRazorPages();

app.Run();


async Task SeedRoles(RoleManager<IdentityRole>? roleManager)
{
    if (roleManager is null)
        return;
    
    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));

    if (!await roleManager.RoleExistsAsync("User"))
        await roleManager.CreateAsync(new IdentityRole("User"));
}