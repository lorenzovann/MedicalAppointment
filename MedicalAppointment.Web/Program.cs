using MedicalAppointment.Application.Contracts.appointmentsContracts;
using MedicalAppointment.Application.Services.appointmentsService;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.appointments;
using MedicalAppointment.Persistance.Repositories.appointmentsRepositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MedicalAppointmentContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MedicalAppointmentContext")));

//Registro de cada una de las dependencias repositorios de appointments
builder.Services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();


//Registro de cada una de las dependencias servicios de appointments
builder.Services.AddScoped<IAppointmentsService, AppointmentsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
