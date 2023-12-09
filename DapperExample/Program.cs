using DapperExample.Infrastructure.Repositories;
using System.Globalization;

//CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
//CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
//CultureInfo culture = new CultureInfo("en-US");
//CultureInfo.DefaultThreadCurrentCulture = culture;
//CultureInfo.DefaultThreadCurrentUICulture = culture;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<MarkRepository>();

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
