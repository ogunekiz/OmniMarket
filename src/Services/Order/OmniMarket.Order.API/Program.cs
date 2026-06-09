using OmniMarket.Order.API.Extensions;
using OmniMarket.Order.Application;
using OmniMarket.Order.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.ApplyMigrations();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
