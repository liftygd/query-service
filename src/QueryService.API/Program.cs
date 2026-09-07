using Application;
using Infrastructure;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Сервисы
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});

var app = builder.Build();

// Middleware
app.UseCustomExceptionHandling();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

// Инициализация базы данных
await app.InitializeDatabaseAsync<BaseDbContext>();

app.Run();