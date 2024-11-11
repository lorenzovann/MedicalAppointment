using Medical.Percistances.cs.Context;
using MedicalAppointment.Persistance.Interfaces;
using MedicalAppointment.Persistance.Interfaces.Configuration.UsersInterfaces;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using Microsoft.EntityFrameworkCore;
using MedicalCore.IOC.cs.Dependecy.Configurations; 
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
  builder.Services.AddDbContext<MedicalContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("MedicalContext")));

// agrego archivos de depencias

builder.Services.AddConfigurationsDependecy(); 





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
          