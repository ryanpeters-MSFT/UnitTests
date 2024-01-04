using UnitTests.Common.Repositories;
using UnitTests.Data;
using UnitTests.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ClientService>();
builder.Services.AddTransient<IClientRepository, EntityFrameworkClientRepository>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
