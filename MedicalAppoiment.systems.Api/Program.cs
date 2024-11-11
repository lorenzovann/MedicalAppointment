using Medical.Percistances.cs.Context;
using MedicalAppointment.Persistance.Interfaces.Configuration.SystemIntefaces;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using MedicalCoreAplications.cs.Contracts.systems;
using MedicalIOC.cs.Dependecy.Configurations;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MedicalContext>(options => options.UseSqlServer
(builder.Configuration.GetConnectionString("MedicalContext")));

// aqui agrego las inyecciones de depencia 

builder.Services.AddSystemDependecy(); 




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

app.UseAuthorization();

app.MapControllers();

app.Run();
