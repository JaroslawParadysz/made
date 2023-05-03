using Made.Application;
using Made.Application.Dto;
using Made.Domain;
using Made.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(
    opt => opt.UseNpgsql(configuration.GetConnectionString("MadeDatabase")));

builder.Services.AddTransient<IService, Service>();

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

app.MapPost("allocate", async (AllocateDto dto, IService service) =>
{
    Guid allocatedOrderLineId;
    try
    {
        allocatedOrderLineId = await service.AllocateAsync(dto);
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }
    
    return Results.Created($"/order-lines/{allocatedOrderLineId}", null);
});

app.Run();