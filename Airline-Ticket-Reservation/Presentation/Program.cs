using Application.Contracts;
using Application.Services;
using DataAccess.Contracts;
using DataAccess.DataContext;
using DataAccess.Repositories;
using DataAccess.Triggers;
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
    connection = builder.Configuration.GetConnectionString("SQLAZURECONNSTR_DefaultConnection");
}

builder.Services.AddDbContext<AirlineDbContext>(options =>
{
    options.UseSqlServer(connection);
    options.UseTriggers(triggerOptions =>
        triggerOptions.AddTrigger<CreateSeatsAfterFlightAdded>());
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AirlineDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRepository<Ticket>, TicketDbRepository>();
builder.Services.AddScoped<IRepository<Flight>, FlightDbRepository>();
builder.Services.AddScoped<IRepository<Seat>, SeatDbRepository>();
builder.Services.AddScoped<IRepository<Passport>, PassportDbRepository>();
builder.Services.AddScoped<ITicketService, AirlineService>();
builder.Services.AddScoped<ISeatService, AirlineService>();
builder.Services.AddScoped<IFlightService, AirlineService>();
builder.Services.AddScoped<IAirlineService, AirlineService>();
builder.Services.AddScoped<IPassportService, PassportService>();
builder.Services.AddSingleton<FileService>();

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
