using EventRegistration.Application.Helpers;
using EventRegistration.Application.Interfaces;
using EventRegistration.Application.Services;
using EventRegistration.Infra;
using EventRegistration.Infra.Interfaces;
using EventRegistration.Infra.Repos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Register the ApplicationDbContext for Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services for dependency injection
builder.Services.AddScoped<IEventRepo, EventRepo>();
builder.Services.AddScoped<IParticipantRepo, ParticipantRepo>();
builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
builder.Services.AddScoped<IEventParticipantRepository, EventParticipantRepository>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();


// Register AutoMapper and add the MappingProfile
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Event}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Participant}/{action=Index}/{id?}");


app.Run();
