using Application.Contracts;
using Application.Services;
using DataAccess;
using DataAccess.Contracts;
using DataAccess.DataContext;
using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connection;
if (builder.Environment.IsDevelopment())
{
    connection = builder.Configuration.GetConnectionString("DefaultConnection");
}
else
{
    // An environment variable is set in the Azure App Service that contains the connection string
    connection = builder.Configuration.GetConnectionString("SQLAZURECONNSTR_DefaultConnection");
}

builder.Services.AddDbContext<AirlineDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AirlineDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ITicketsRepository, TicketDbRepository>();
builder.Services.AddScoped<IFlightsRepository, FlightDbRepository>();
builder.Services.AddScoped<ISeatsRepository, SeatDbRepository>();
builder.Services.AddScoped<IPassportRepository, PassportDbRepository>();
builder.Services.AddScoped<IAirlineService, AirlineService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<FileService>();

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