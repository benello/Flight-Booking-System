using DataAccess.Contracts;
using DataAccess.DataContext;
using DataAccess.Repositories;
using Domain.Models;
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
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING") 
                 ?? throw new InvalidOperationException("Missing connection string for Azure SQL");
}

builder.Services.AddDbContext<AirlineDbContext>(options =>
    options.UseSqlServer(connection));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AirlineDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ITickets, TicketDbRepository>();
builder.Services.AddScoped<IFlights, FlightDbRepository>();

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
app.MapRazorPages();

app.Run();
