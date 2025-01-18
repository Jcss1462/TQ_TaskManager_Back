using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TQ_TaskManager_Back.Contexts;
using TQ_TaskManager_Back.Services;

var builder = WebApplication.CreateBuilder(args);



// Configurar el servicio de DbContext
builder.Services.AddDbContext<TQContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("tqBd")));

builder.Services.AddScoped<IRolService, RolService>();

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
