using QueryService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add Services
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.Run();