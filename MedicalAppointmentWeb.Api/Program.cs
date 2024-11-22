using MedicalAppointment.Application.Contracts.appointmentsContracts;
using MedicalAppointment.Application.Services.appointmentsService;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.appointments;
using MedicalAppointment.Persistance.Repositories.appointmentsRepositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MedicalAppointmentContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MedicalAppointmentContext")));

// Agregando dependencias de los repositorios
builder.Services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
builder.Services.AddScoped<IDoctorAvailabilityRepository, DoctorAvailabilityRepository>();
// Agregando dependencias de los servicios 
builder.Services.AddTransient<IAppointmentsService, AppointmentsService>();
builder.Services.AddTransient<IDoctorAvailabilityService, DoctorAvailabilityService>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
