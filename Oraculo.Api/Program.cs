using Oraculo.Application;
using Oraculo.Application.Interfaces.TelegramBot;
using Oraculo.Infrastructure;
using Oraculo.Infrastructure.External;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddHttpClient<ITelegramBotClient, TelegramBotClient>();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    //options.AddDefaultPolicy(policy =>
    //{
    //    policy.WithOrigins().AllowAnyHeader().AllowAnyMethod();
    //});
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("openapi");
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
