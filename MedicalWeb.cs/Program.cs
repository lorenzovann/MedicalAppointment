using Medical.Percistances.cs.Context;
using MedicalAppointment.Persistance.Interfaces.Configuration.SystemIntefaces;
using MedicalAppointment.Persistance.Interfaces.Configurations;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using MedicalCoreAplications.cs.Contracts.Configurations;
using MedicalCoreAplications.cs.Contracts.Systems;
using MedicalCoreAplications.cs.Services;
using MedicalCoreAplications.cs.Services.Configurations;
using MedicalCoreAplications.cs.Services.systems;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); 



builder.Services.AddDbContext<MedicalContext>(options => options.UseSqlServer
(builder.Configuration.GetConnectionString("MedicalContext")));

builder.Services.AddScoped<IStatusRepository, StatusRepositorie>();
builder.Services.AddTransient<IStatusServices, StatusServices>();
// aqui agrego las inyecciones de depencia 

builder.Services.AddScoped<INotificationsRepository, NotificationsRepositories>();
builder.Services.AddTransient<INotificationServices, NotificationsServices>();

builder.Services.AddScoped<IRoleRepository, RoleRepositorie>();
builder.Services.AddTransient<IRoleServices, RoleServices>(); 


builder.Services.AddScoped<IUserRepository, UserRepositorie>();
builder.Services.AddTransient<IUserServices, UserServices>(); 


builder.Services.AddScoped<IDoctorRepository, DoctorRepositorie>();
builder.Services.AddTransient<IDoctorServices, DoctorServices>();   

        

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
