using hafenix_proj_backend.BL.Interface;
using hafenix_proj_backend.BL.Repository;
using hafenix_proj_backend.Data.Interface;
using hafenix_proj_backend.Data.Repository;
using hafenix_proj_backend.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<HttpClientService>(client =>
{
    client.BaseAddress = new Uri("https://openBanking/");
});
builder.Services.AddScoped<ITransactionBL, TransactionBL>();
builder.Services.AddScoped<ITransactionData, TransactionData>();

// only for test i use AllowAll
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});






var app = builder.Build();

app.MapGet("/", () => "Welcome to the API!");

app.UseHttpsRedirection();

// Allways should be before MapControllers
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
